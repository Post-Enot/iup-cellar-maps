using IUP.Toolkits.Matrices;
using System;
using System.Collections.Generic;
using UnityEngine;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Клеточная карта.
    /// </summary>
    public sealed class CellarMap : ICellarMap
    {
        public CellarMap(int width, int height, string defaultLayerName)
        {
            _layers = new(width, height, defaultLayerName);
        }

        public int Width => _layers.Width;
        public int Height => _layers.Height;
        public int LayersCount => _layers.Count;
        public IPalette Palette => _palette;

        private readonly Palette _palette = new();
        private readonly LayerAtlas _layers;

        public ILayer this[int layerIndex] => _layers[layerIndex];

        public void Recreate(int width, int height)
        {
            _layers.RecreateAllLayers(width, height);
        }

        public void Resize(
            int width,
            int height,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule)
        {
            _layers.ResizeAllLayers(width, height, widthResizeRule, heightResizeRule);
        }

        public void Rotate(MatrixRotation matrixRotation)
        {
            _layers.RotateAllLayers(matrixRotation);
        }

        public void Mirror(MatrixMirror matrixMirror)
        {
            _layers.MirrorAllLayers(matrixMirror);
        }

        public void AddCellTypeToPalette(string cellTypeName)
        {
            _palette.Add(cellTypeName);
        }

        public void RemoveCellTypeFromPalette(string cellTypeName)
        {
            ICellType cellType = _palette[cellTypeName];
            _palette.Remove(cellTypeName);
            _layers.ClearAllLayersFrom(cellType);
        }

        public void AddLayer(string layerName)
        {
            _layers.Add(layerName);
        }

        public void RemoveLayer(int layerIndex)
        {
            _layers.Remove(layerIndex);
        }

        public void SetCellType(int layerIndex, int x, int y, string cellTypeName)
        {
            if (cellTypeName != null)
            {
                if (!_palette.Contains(cellTypeName))
                {
                    throw new ArgumentException(
                        "Клеткам клеточной карты можно присвоить только тип, содержащийся в палитре.");
                }
                ICellType cellType = _palette[cellTypeName];
                _layers.GetLayer(layerIndex).GetCell(x, y).SetCellType(cellType);
            }
            else
            {
                _layers.GetLayer(layerIndex).GetCell(x, y).SetCellType(null);
            }
        }

        public void SetCellType(int layerIndex, Vector2Int coordinate, string cellTypeName)
        {
            SetCellType(layerIndex, coordinate.x, coordinate.y, cellTypeName);
        }

        public void SetCellUniqueData(int layerIndex, int x, int y, string uniqueData)
        {
            _layers.GetLayer(layerIndex).GetCell(x, y).UniqueData = uniqueData;
        }

        public void SetCellUniqueData(int layerIndex, Vector2Int coordinate, string uniqueData)
        {
            SetCellUniqueData(layerIndex, coordinate.x, coordinate.y, uniqueData);
        }

        /// <summary>
        /// Преобразовывает клеточную карту в DTO-объект.
        /// </summary>
        /// <returns>Возвращает DTO-объект клеточной карты.</returns>
        public DTO.CellarMap ToDTO()
        {
            DTO.CellarMap cellarMapDTO = new()
            {
                width = Width,
                height = Height,
                cell_types = new DTO.CellType[Palette.Count],
                layers = new DTO.Layer[LayersCount]
            };

            // Сериализация палитры:
            List<ICellType> cellTypes = new(Palette.Count);
            Dictionary<string, int> cellTypeIndexByCellTypeName = new();
            foreach (ICellType cellType in Palette)
            {
                cellTypes.Add(cellType);
            }
            for (int i = 0; i < cellarMapDTO.cell_types.Length; i += 1)
            {
                cellarMapDTO.cell_types[i] = new DTO.CellType()
                {
                    type_name = cellTypes[i].TypeName
                };
                cellTypeIndexByCellTypeName.Add(cellTypes[i].TypeName, i);
            }

            // Сериализация слоёв:
            for (int i = 0; i < LayersCount; i += 1)
            {
                ILayer layer = this[i];
                List<DTO.Cell> cellsDTO = new();
                cellarMapDTO.layers[i] = new DTO.Layer()
                {
                    layer_name = layer.Name
                };
                for (int y = 0; y < layer.Height; y += 1)
                {
                    for (int x = 0; x < layer.Width; x += 1)
                    {
                        ICell cell = layer[x, y];
                        if (!cell.IsEmpty)
                        {
                            DTO.Cell cellDTO = new()
                            {
                                unique_data = cell.UniqueData,
                                coordinate = new Vector2Int(x, y)
                            };
                            if (cell.HasCellType)
                            {
                                cellDTO.cell_type_index = cellTypeIndexByCellTypeName[cell.CellType.TypeName];
                            }
                            else
                            {
                                cellDTO.cell_type_index = -1;
                            }
                            cellsDTO.Add(cellDTO);
                        }
                    }
                }
                cellarMapDTO.layers[i].cells = cellsDTO.ToArray();
            }
            return cellarMapDTO;
        }

        /// <summary>
        /// Преобразовывает DTO объект клеточной карты в клеточную карту.
        /// </summary>
        /// <param name="cellarMapDTO">DTO объект клеточной карты.</param>
        /// <returns>Возвращает клеточную карту.</returns>
        public static ICellarMap DTO_ToCellarMap(DTO.CellarMap cellarMapDTO)
        {
            var cellarMap = new CellarMap(
                cellarMapDTO.width,
                cellarMapDTO.height,
                cellarMapDTO.layers[0].layer_name);

            // Десериализация палитры:
            Dictionary<int, string> cellTypeNameByIndex = new();
            for (int i = 0; i < cellarMapDTO.cell_types.Length; i += 1)
            {
                string typeName = cellarMapDTO.cell_types[i].type_name;
                cellarMap.AddCellTypeToPalette(typeName);
                cellTypeNameByIndex.Add(i, typeName);
            }

            // Десериализация слоёв:
            for (int i = 1; i < cellarMapDTO.layers.Length; i += 1)
            {
                cellarMap.AddLayer(cellarMapDTO.layers[i].layer_name);
            }

            // Десериализация клеток:
            for (int layerIndex = 0; layerIndex < cellarMapDTO.layers.Length; layerIndex += 1)
            {
                DTO.Layer layer = cellarMapDTO.layers[layerIndex];
                for (int cellIndex = 0; cellIndex < layer.cells.Length; cellIndex += 1)
                {
                    DTO.Cell cell = layer.cells[cellIndex];
                    if (cell.cell_type_index != -1)
                    {
                        string cellTypeName = cellTypeNameByIndex[cell.cell_type_index];
                        cellarMap.SetCellType(layerIndex, cell.coordinate, cellTypeName);
                    }
                }
            }
            return cellarMap;
        }
    }
}
