using System;
using IUP.Toolkits.Matrices;
using UnityEngine;

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
        public IReadOnlyPalette Palette => _palette;

        private readonly Palette _palette = new();
        private readonly LayerAtlas _layers;

        public IReadOnlyLayer this[int layerIndex] => _layers[layerIndex];

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
            IReadOnlyCellType cellType = _palette[cellTypeName];
            _palette.Remove(cellTypeName);
            _layers.ClearAllLayersFrom(cellType);
        }

        public void RenameCellType(string oldCellTypeName, string newCellTypeName)
        {
            _palette.RenameCellType(oldCellTypeName, newCellTypeName);
        }

        public void AddLayer(string layerName)
        {
            _layers.Add(layerName);
        }

        public void RemoveLayer(int layerIndex)
        {
            _layers.Remove(layerIndex);
        }

        public void RenameLayer(int layerIndex, string newLayerName)
        {
            _layers.GetLayer(layerIndex).Rename(newLayerName);
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
                IReadOnlyCellType cellType = _palette[cellTypeName];
                _layers.GetLayer(layerIndex).GetCell(x, y).SetType(cellType);
            }
            else
            {
                _layers.GetLayer(layerIndex).GetCell(x, y).SetType(null);
            }
        }

        public void SetCellType(int layerIndex, Vector2Int coordinate, string cellTypeName)
        {
            SetCellType(layerIndex, coordinate.x, coordinate.y, cellTypeName);
        }

        public void SetCellUniqueData(int layerIndex, int x, int y, string uniqueData)
        {
            _layers.GetLayer(layerIndex).GetCell(x, y).Metadata = uniqueData;
        }

        public void SetCellUniqueData(int layerIndex, Vector2Int coordinate, string uniqueData)
        {
            SetCellUniqueData(layerIndex, coordinate.x, coordinate.y, uniqueData);
        }
    }
}
