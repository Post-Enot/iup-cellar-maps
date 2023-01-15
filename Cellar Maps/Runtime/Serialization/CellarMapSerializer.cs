using System.Collections.Generic;
using IUP.Toolkits.CellarMaps.Serialization.DTO;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Serialization
{
    /// <summary>
    /// Класс с набором статичных методов для сериализации и десериализации клеточных карт.
    /// </summary>
    public static class CellarMapSerializer
    {
        /// <summary>
        /// Последняя версия формата данных клеточной карты.
        /// </summary>
        public const int LastDataFormatVersion = 0;

        /// <summary>
        /// Преобразовывает клеточную карту в DTO-объект.
        /// </summary>
        /// <param name="cellarMap">Клеточная карта.</param>
        /// <returns>Возвращает DTO-объект клеточной карты.</returns>
        public static DTO.CellarMap CellarMapToDTO(IReadOnlyCellarMap cellarMap)
        {
            DTO.CellarMap cellarMapDTO = new()
            {
                width = cellarMap.Width,
                height = cellarMap.Height,
                cell_types = new DTO.CellType[cellarMap.Palette.Count],
                layers = new DTO.Layer[cellarMap.LayersCount]
            };

            // Сериализация палитры:
            List<IReadOnlyCellType> cellTypes = new(cellarMap.Palette.Count);
            Dictionary<string, int> cellTypeIndexByCellTypeName = new();
            foreach (IReadOnlyCellType cellType in cellarMap.Palette)
            {
                cellTypes.Add(cellType);
            }
            for (int i = 0; i < cellarMapDTO.cell_types.Length; i += 1)
            {
                cellarMapDTO.cell_types[i] = new DTO.CellType()
                {
                    type_name = cellTypes[i].Name
                };
                cellTypeIndexByCellTypeName.Add(cellTypes[i].Name, i);
            }

            // Сериализация слоёв:
            for (int i = 0; i < cellarMap.LayersCount; i += 1)
            {
                IReadOnlyLayer layer = cellarMap[i];
                List<DTO.Cell> cellsDTO = new();
                cellarMapDTO.layers[i] = new DTO.Layer()
                {
                    layer_name = layer.Name
                };
                for (int y = 0; y < layer.Height; y += 1)
                {
                    for (int x = 0; x < layer.Width; x += 1)
                    {
                        IReadOnlyCell cell = layer[x, y];
                        if (!cell.IsEmpty)
                        {
                            DTO.Cell cellDTO = new()
                            {
                                unique_data = cell.Metadata,
                                coordinate = new Vector2Int(x, y)
                            };
                            if (cell.HasType)
                            {
                                cellDTO.cell_type_index = cellTypeIndexByCellTypeName[cell.Type.Name];
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

        public static CellarMapFile JsonToCellarMapFileDTO(string cellarMapJson)
        {
            return JsonUtility.FromJson<CellarMapFile>(cellarMapJson);
        }

        public static string CellarMapFileDTO_ToJson(CellarMapFile cellarMapFileDTO)
        {
            return JsonUtility.ToJson(cellarMapFileDTO, true);
        }
    }
}
