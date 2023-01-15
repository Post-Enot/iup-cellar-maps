using IUP.Toolkits.CellarMaps.Editor.UI;
using IUP.Toolkits.Matrices;
using System;
using System.Text;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Класс-посредник между вариантом взаимодействия и UI-представлением клеточной карты.
    /// </summary>
    public sealed class CellarMapPresenter
    {
        public CellarMapPresenter(
            ICellarMapInteractor model,
            UI.ICellarMap view,
            float colorLightnessDelta,
            string emptyCellSignature,
            Color emptyCellColor,
            string uniqueDataTitleIndicator)
        {
            _model = model;
            _view = view;
            _view.Clicked += (Vector2Int cellCoordinate) => Clicked?.Invoke(cellCoordinate);
            _colorLightnessDelta = colorLightnessDelta;
            _emptyCellSignature = emptyCellSignature;
            _emptyCellColor = emptyCellColor;
            _uniqueDataTitleIndicator = uniqueDataTitleIndicator;
        }

        /// <summary>
        /// Индекс активного слоя.
        /// </summary>
        public int ActiveLayerIndex { get; private set; }

        public event Action<Vector2Int> Clicked;

        private readonly ICellarMapInteractor _model;
        private readonly UI.ICellarMap _view;
        private readonly float _colorLightnessDelta;
        private readonly string _emptyCellSignature;
        private readonly Color _emptyCellColor;
        private readonly string _uniqueDataTitleIndicator;
        private readonly Matrix<CellViewData> _cellarMapViewData = new();

        /// <summary>
        /// Устанавливает индекс активного слоя, по которому происходит обновление представления 
        /// клеточной карты.
        /// </summary>
        /// <param name="activeLayerIndex">Индекс активного слоя.</param>
        public void SetActiveLayerIndex(int activeLayerIndex)
        {
            ActiveLayerIndex = activeLayerIndex;
        }

        /// <summary>
        /// Обновляет представление клетки клеточной карты.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void UpdateCellView(int x, int y)
        {
            (IReadOnlyCell cell, int cellLayerIndex) = _model.GetTopCell(x, y, ActiveLayerIndex);
            CellViewData viewData = CreateCellViewData(cell, x, y, cellLayerIndex);
            if (_cellarMapViewData[x, y] != viewData)
            {
                _cellarMapViewData[x, y] = viewData;
                _view.UpdateCellViewData(x, y, viewData);
            }
        }

        /// <summary>
        /// Обновляет представление клетки клеточной карты.
        /// </summary>
        /// <param name="coordinate"></param>
        public void UpdateCellView(Vector2Int coordinate)
        {
            UpdateCellView(coordinate.x, coordinate.y);
        }

        /// <summary>
        /// Обновляет представление клеточной карты.
        /// </summary>
        public void UpdateCellarMapView()
        {
            if ((_cellarMapViewData.Width != _model.Width) || (_cellarMapViewData.Height != _model.Height))
            {
                _cellarMapViewData.Recreate(_model.Width, _model.Height);
                _cellarMapViewData.InitAllElements(() => new CellViewData());
                _view.Recreate(_model.Width, _model.Height);
            }
            for (int y = 0; y < _model.Height; y += 1)
            {
                for (int x = 0; x < _model.Width; x += 1)
                {
                    UpdateCellView(x, y);
                }
            }
        }

        private CellViewData CreateCellViewData(
            IReadOnlyCell cell,
            int cellX_Position,
            int cellY_Position,
            int cellLayerIndex)
        {
            return new CellViewData()
            {
                Color = CreateCellColor(cell, cellLayerIndex),
                Title = CreateTitle(cell),
                Tooltip = CreateTooltip(cell, cellX_Position, cellY_Position, cellLayerIndex)
            };
        }

        private Color CreateCellColor(IReadOnlyCell cell, int cellLayerIndex)
        {
            if (cell.HasType)
            {
                ICellTypeViewData cellTypeViewData = _model.CellTypesViewData[cell.Type.Name];
                Color color = cellTypeViewData.Color;
                if (cellLayerIndex != ActiveLayerIndex)
                {
                    color = ChangeColorLightness(color);
                }
                return color;
            }
            return _emptyCellColor;
        }

        private string CreateTitle(IReadOnlyCell cell)
        {
            if (cell.HasMetadata)
            {
                return _uniqueDataTitleIndicator;
            }
            return string.Empty;
        }

        private Color ChangeColorLightness(Color color)
        {
            Color.RGBToHSV(color, out float H, out float S, out float V);
            if (V >= 0.5f)
            {
                V -= _colorLightnessDelta;
            }
            else
            {
                V += _colorLightnessDelta;
            }
            return Color.HSVToRGB(H, S, V);
        }

        private string CreateTooltip(
            IReadOnlyCell cell,
            int cellX_position,
            int cellY_position,
            int cellLayerIndex)
        {
            var builder = new StringBuilder();
            string signature = cell.Type?.Name ?? _emptyCellSignature;
            builder.AppendLine($"({cellX_position}, {cellY_position})");
            if (cell.HasType)
            {
                builder.AppendLine($"Type: {signature}");
            }
            if (!cell.IsEmpty)
            {
                string cellLayerName = _model[cellLayerIndex].Name;
                builder.AppendLine($"Layer: {cellLayerName} ({cellLayerIndex})");
            }
            if (cell.HasMetadata)
            {
                builder.AppendLine("Has unique data.");
            }
            return builder.ToString().TrimEnd();
        }
    }
}
