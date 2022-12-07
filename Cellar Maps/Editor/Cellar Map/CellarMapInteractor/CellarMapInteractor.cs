using IUP.Toolkits.Matrices;
using System;
using UnityEngine;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Вариант использования клеточной карты.
    /// </summary>
    public sealed class CellarMapInteractor : ICellarMapInteractor
    {
        /// <summary>
        /// Инициализирует вариант использования клеточной карты.
        /// </summary>
        /// <param name="cellarMap">Клеточная карта.</param>
        public CellarMapInteractor(ICellarMap cellarMap)
        {
            _cellarMap = cellarMap;
        }

        public int Width => _cellarMap.Width;
        public int Height => _cellarMap.Height;
        public int LayersCount => _cellarMap.LayersCount;
        public ICellTypesViewData CellTypesViewData => _cellTypesViewData;
        public ILayersViewData LayersViewData => _layersViewData;
        public IPalette Palette => _cellarMap.Palette;

        private readonly ICellarMap _cellarMap;
        private readonly LayersViewData _layersViewData = new();
        private readonly CellTypesViewData _cellTypesViewData = new();
        private Action _markUnsavedChanges;

        public ILayer this[int layerIndex] => _cellarMap[layerIndex];

        public void SetMarkUnsavedChangesCallback(Action markUnsavedCallback)
        {
            _markUnsavedChanges = markUnsavedCallback;
        }

        public void Recreate(int width, int height)
        {
            _cellarMap.Recreate(width, height);
            _markUnsavedChanges();
        }

        public void Resize(
            int widthOffset,
            int heightOffset,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule)
        {
            _cellarMap.Resize(widthOffset, heightOffset, widthResizeRule, heightResizeRule);
            _markUnsavedChanges();
        }

        public void Rotate(MatrixRotation matrixRotation)
        {
            _cellarMap.Rotate(matrixRotation);
        }

        public (ICell cell, int cellLayerIndex) GetTopCell(int x, int y, int startLayerIndex = 0)
        {
            ICell cell = this[startLayerIndex][x, y];
            while (startLayerIndex > 0 && cell.IsEmpty)
            {
                startLayerIndex -= 1;
                cell = this[startLayerIndex][x, y];
            }
            return (cell, startLayerIndex);
        }

        public (ICell cell, int cellLayerIndex) GetTopCell(Vector2Int coordinate, int startLayerIndex = 0)
        {
            return GetTopCell(coordinate.x, coordinate.y, startLayerIndex);
        }

        public void AddCellTypeToPalette(string cellTypeName, Color cellTypeColor)
        {
            _cellarMap.AddCellTypeToPalette(cellTypeName);
            _cellTypesViewData.Add(cellTypeName, cellTypeColor);
            _markUnsavedChanges();
        }

        public void RemoveCellTypeFromPalette(string cellTypeName)
        {
            _cellarMap.RemoveCellTypeFromPalette(cellTypeName);
            _cellTypesViewData.Remove(cellTypeName);
            _markUnsavedChanges();
        }

        public void AddLayer(string layerName, Color layerColor)
        {
            _cellarMap.AddLayer(layerName);
            _layersViewData.Add(layerName, layerColor);
            _markUnsavedChanges();
        }

        public void RemoveLayer(int layerIndex)
        {
            _layersViewData.Remove(layerIndex);
            _cellarMap.RemoveLayer(layerIndex);
            _markUnsavedChanges();
        }

        public void SetCellType(int layerIndex, int x, int y, string cellTypeName)
        {
            _cellarMap.SetCellType(layerIndex, x, y, cellTypeName);
            _markUnsavedChanges();
        }

        public void SetCellType(int layerIndex, Vector2Int coordinate, string cellTypeName)
        {
            SetCellType(layerIndex, coordinate.x, coordinate.y, cellTypeName);
            _markUnsavedChanges();
        }

        public void SetCellUniqueData(int layerIndex, int x, int y, string uniqueData)
        {
            _cellarMap.SetCellUniqueData(layerIndex, x, y, uniqueData);
            _markUnsavedChanges();
        }

        public void SetCellUniqueData(int layerIndex, Vector2Int coordinate, string uniqueData)
        {
            SetCellUniqueData(layerIndex, coordinate.x, coordinate.y, uniqueData);
            _markUnsavedChanges();
        }

        public bool RenameCellType(string cellTypeName, string newTypeName)
        {
            bool isRenamed = _cellarMap.Palette.RenameCellType(cellTypeName, newTypeName);
            if (isRenamed)
            {
                _cellTypesViewData.RenameCellTypeViewData(cellTypeName, newTypeName);
                _markUnsavedChanges();
            }
            return isRenamed;
        }

        public void SetCellTypeColor(string cellTypeName, Color cellTypeColor)
        {
            _cellTypesViewData.SetCellTypeColor(cellTypeName, cellTypeColor);
            _markUnsavedChanges();
        }

        public void RenameLayer(int layerIndex, string newLayerName)
        {
            _cellarMap[layerIndex].Rename(newLayerName);
            _markUnsavedChanges();
        }

        public void SetLayerColor(int layerIndex, Color layerColor)
        {
            _layersViewData[layerIndex].Color = layerColor;
            _markUnsavedChanges();
        }

        public (DTO.CellarMapViewData, DTO.CellarMap) ToDTO()
        {
            var cellarMapViewDataDTO = new DTO.CellarMapViewData()
            {
                cell_types_view_data = new DTO.CellTypeViewData[CellTypesViewData.Count],
                layers_view_data = new DTO.LayerViewData[LayersViewData.Count]
            };
            for (int i = 0; i < CellTypesViewData.Count; i += 1)
            {
                cellarMapViewDataDTO.cell_types_view_data[i] = new DTO.CellTypeViewData()
                {
                    cell_type_name = CellTypesViewData[i].CellTypeName,
                    color = CellTypesViewData[i].Color
                };
            }
            for (int i = 0; i < LayersViewData.Count; i += 1)
            {
                cellarMapViewDataDTO.layers_view_data[i] = new DTO.LayerViewData()
                {
                    color = LayersViewData[i].Color,
                    layer_name = LayersViewData[i].Name
                };
            }
            return (cellarMapViewDataDTO, _cellarMap.ToDTO());
        }

        public static ICellarMapInteractor DTO_ToCellarMapInteractor(
            DTO.CellarMapFile cellarMapFileDTO)
        {
            ICellarMap cellarMap = CellarMap.DTO_ToCellarMap(cellarMapFileDTO.cellar_map);
            DTO.CellarMapViewData cellarMapViewDataDTO = cellarMapFileDTO.cellar_map_view_data;
            var cellarMapInteractor = new CellarMapInteractor(cellarMap);
            foreach (DTO.CellTypeViewData viewData in cellarMapViewDataDTO.cell_types_view_data)
            {
                cellarMapInteractor._cellTypesViewData.Add(viewData.cell_type_name, viewData.color);
            }
            foreach (var viewData in cellarMapViewDataDTO.layers_view_data)
            {
                cellarMapInteractor._layersViewData.Add(viewData.layer_name, viewData.color);
            }
            return cellarMapInteractor;
        }
    }
}
