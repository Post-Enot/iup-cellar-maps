using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class CellarMapCell : Button
    {
        public CellarMapCell()
        {
            _defaultBackgroundColor = style.backgroundColor;
        }

        public CellarMapCell(int x, int y, Action<Vector2Int> interactionCallback)
        {
            Coordinate = new Vector2Int(x, y);
            AddToClassList("cm-cellar-map-cell");
            UpdateTooltip();
            clicked += InvokeInteractionCallback;
            _defaultBackgroundColor = style.backgroundColor;
            _interactionCallback = interactionCallback;
        }

        ~CellarMapCell()
        {
            if (_viewData != null)
            {
                _viewData.ViewDataUpdated -= InitViewData;
                clicked -= InvokeInteractionCallback;
            }
        }

        public Vector2Int Coordinate { get; }
        public CellTypeViewData ViewData
        {
            get => _viewData;
            set
            {
                if (_viewData != null)
                {
                    _viewData.ViewDataUpdated -= InitViewData;
                }
                _viewData = value;
                if (_viewData != null)
                {
                    _viewData.ViewDataUpdated += InitViewData;
                }
                InitViewData();
            }
        }
        public CellType CellType => _viewData?.Type;

        private readonly StyleColor _defaultBackgroundColor;
        private readonly Action<Vector2Int> _interactionCallback;

        private CellTypeViewData _viewData;

        private void InitViewData()
        {
            if (_viewData != null)
            {
                style.backgroundColor = new StyleColor(_viewData.Color);
            }
            else
            {
                style.backgroundColor = _defaultBackgroundColor;
            }
            UpdateTooltip();
        }

        private void UpdateTooltip()
        {
            tooltip = $"{Coordinate}\n";
            if (_viewData == null)
            {
                tooltip += "null";
            }
            else
            {
                tooltip += $"{_viewData.TypeName}";
            }
        }

        private void InvokeInteractionCallback()
        {
            _interactionCallback(Coordinate);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellarMapCell, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
