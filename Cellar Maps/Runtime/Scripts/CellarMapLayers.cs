using IUP.Toolkits.Matrices;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    [Serializable]
    public sealed class CellarMapLayers : ICellarMapLayers, ISerializationCallbackReceiver
    {
        public CellarMapLayers(int layersWidth, int layersHeight)
        {
            _layersWidth = layersWidth;
            _layersHeight = layersHeight;
            _layers = new();
        }

        public int Width => _layersWidth;
        public int Height => _layersHeight;
        public int Count => _layers.Count;
        public IReadOnlyList<ICellarMapLayer> Layers => _layers;

        public event Action LayerAdded;
        public event Action LayerRemoved;
        public event Action CellsChanged;

        [SerializeField] private int _layersWidth;
        [SerializeField] private int _layersHeight;
        [SerializeReference] private List<CellarMapLayer> _layers;

        public ICellarMapLayer this[int index] => _layers[index];

        public void AddLayer()
        {
            var layer = new CellarMapLayer(_layersWidth, _layersHeight);
            layer.CellsChanged += HandleCellsChangingOnLayer;
            _layers.Add(layer);
            LayerAdded?.Invoke();
        }

        /// <summary>
        /// Если количество слоёв больше 1, удаляет слой по переданному индексу; иначе очищает единственный 
        /// оставшийся слой, присваивая всем клеткам значение null.
        /// </summary>
        /// <param name="layerIndex">Индекс удаляемого слоя.</param>
        public void RemoveLayer(int layerIndex)
        {
            if (_layers.Count > 1)
            {
                _layers[layerIndex].CellsChanged -= HandleCellsChangingOnLayer;
                _layers.RemoveAt(layerIndex);
                LayerRemoved?.Invoke();
            }
            else
            {
                _layers[0].Clear();
            }
        }

        public void MoveLayerFromTo(int from, int to)
        {
            CellarMapLayer item = _layers[from];
            _layers.RemoveAt(from);
            _layers.Insert(to, item);
        }

        public void FillAllLayers(CellType type)
        {
            foreach (CellarMapLayer layer in _layers)
            {
                layer.Matrix.InitAllElements(() => type);
            }
            CellsChanged?.Invoke();
        }

        public void ClearAllLayers()
        {
            FillAllLayers(null);
        }

        public void ReplaceWithOtherInAllLayers(CellType replace, CellType other)
        {
            foreach (CellarMapLayer layer in _layers)
            {
                layer.Matrix.ForEachElements(
                    delegate (ref CellType element)
                    {
                        if (element == replace)
                        {
                            element = other;
                        }
                    });
            }
            CellsChanged?.Invoke();
        }

        public void ClearAllLayersFrom(CellType type)
        {
            ReplaceWithOtherInAllLayers(type, null);
        }

        /// <summary>
        /// Пересоздаёт все слои, сбрасывая все значения клеток.
        /// </summary>
        /// <param name="width">Ширина слоёв: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоёв: должна быть больше или равна 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public void RecreateAllLayers(int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentException("Ширина слоя должна быть больше или равна 1.");
            }
            if (height < 1)
            {
                throw new ArgumentException("Высота слоя должна быть больше или равна 1.");
            }
            foreach (CellarMapLayer layer in _layers)
            {
                layer.Recreate(width, height);
            }
            _layersHeight = height;
            _layersWidth = width;
        }

        public void ResizeAllLayers(
            int widthOffset,
            int heightOffset,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule)
        {
            foreach (CellarMapLayer layer in _layers)
            {
                layer.Matrix.Resize(widthOffset, heightOffset, widthResizeRule, heightResizeRule);
            }
            _layersWidth = _layers[0].Width;
            _layersHeight = _layers[0].Height;
        }

        private void HandleCellsChangingOnLayer()
        {
            CellsChanged?.Invoke();
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            foreach (CellarMapLayer layer in _layers)
            {
                layer.CellsChanged += HandleCellsChangingOnLayer;
            }
        }
    }
}
