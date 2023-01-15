using System;
using System.Collections;
using System.Collections.Generic;
using IUP.Toolkits.Matrices;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Атлас слоёв клеточной карты. Всегда содержит минимум 1 слой.
    /// </summary>
    public sealed class LayerAtlas : ILayerAtlas
    {
        /// <summary>
        /// Инициализирует слои клеточной карты и создаёт базовый слой.
        /// </summary>
        /// <param name="width">Ширина слоёв: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоёв: должна быть больше или равна 1.</param>
        /// <param name="defaultLayerName"></param>
        public LayerAtlas(int width, int height, string defaultLayerName)
        {
            Layer defaultLayer = new(width, height, defaultLayerName);
            _layerIndexByLayer.Add(defaultLayer, _layers.Count);
            _layers.Add(defaultLayer);
        }

        public int Width => _layers[0].Width;
        public int Height => _layers[0].Height;
        public int Count => _layers.Count;
        public IReadOnlyDictionary<IReadOnlyLayer, int> LayerIndexByLayer => _layerIndexByLayer;

        private readonly List<Layer> _layers = new();
        private readonly Dictionary<IReadOnlyLayer, int> _layerIndexByLayer = new();

        public IReadOnlyLayer this[int layerIndex] => _layers[layerIndex];

        public void Add(string layerName)
        {
            Layer layer = new(Width, Height, layerName);
            _layerIndexByLayer.Add(layer, _layers.Count);
            _layers.Add(layer);
        }

        public void Remove(int layerIndex)
        {
            if (_layers.Count == 1 && layerIndex == 0)
            {
                throw new InvalidOperationException(
                    "Нельзя удалить последний слой: в атласе слоёв всегда должен быть хотя бы 1 слой.");
            }
            Layer removedLayer = _layers[layerIndex];
            _layers.RemoveAt(layerIndex);
            _layerIndexByLayer.Remove(removedLayer);
            ResetLayerIndexes(layerIndex, _layers.Count);

        }

        /// <summary>
        /// Перемещает слой в списке.
        /// </summary>
        public void MoveLayerFromTo(int from, int to)
        {
            _layers.MoveItemFromTo(from, to);
            if (from > to)
            {
                (from, to) = (to, from);
            }
            ResetLayerIndexes(from, to + 1);
        }

        public Layer GetLayer(int layerIndex)
        {
            return _layers[layerIndex];
        }

        /// <summary>
        /// Очищает слои от переданного типа клетки, присваивая всем клеткам этого типа тип null и сбрасывая 
        /// уникальные данные.
        /// </summary>
        /// <param name="type">Тип клетки, от которого очищаются слои.</param>
        public void ClearAllLayersFrom(IReadOnlyCellType type)
        {
            foreach (Layer layer in _layers)
            {
                layer.ClearFrom(type);
            }
        }

        /// <summary>
        /// Пересоздаёт все слои, на каждом сбрасывая все значения клеток.
        /// </summary>
        /// <param name="width">Ширина слоя: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоя: должна быть больше или равна 1.</param>
        public void RecreateAllLayers(int width, int height)
        {
            foreach (Layer layer in _layers)
            {
                layer.Recreate(width, height);
            }
        }

        /// <summary>
        /// Поворачивает все слои.
        /// </summary>
        /// <param name="matrixRotation">Тип вращения матриц слоёв.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RotateAllLayers(MatrixRotation matrixRotation)
        {
            foreach (Layer layer in _layers)
            {
                layer.Rotate(matrixRotation);
            }
        }

        /// <summary>
        /// Изменяет размер всех слоёв в соответствии с переданными правилами.
        /// </summary>
        /// <param name="widthOffset">Величина изменения ширины слоёв. После изменения размера ширина 
        /// слоёв должна быть больше или равна 1.</param>
        /// <param name="heightOffset">Величина изменения высоты слоёв. После изменения высота слоёв 
        /// должна быть больше или равна 1.</param>
        /// <param name="widthResizeRule">Правило изменения ширины слоёв.</param>
        /// <param name="heightResizeRule">Правило изменения высоты слоёв.</param>
        public void ResizeAllLayers(
            int widthOffset,
            int heightOffset,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule)
        {
            foreach (Layer layer in _layers)
            {
                layer.Resize(widthOffset, heightOffset, widthResizeRule, heightResizeRule);
            }
        }

        /// <summary>
        /// Отражает все слои.
        /// </summary>
        /// <param name="matrixMirror">Тип отражения матриц слоёв.</param>
        public void MirrorAllLayers(MatrixMirror matrixMirror)
        {
            foreach (Layer layer in _layers)
            {
                layer.Mirror(matrixMirror);
            }
        }

        public IEnumerator<IReadOnlyLayer> GetEnumerator()
        {
            return _layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _layers.GetEnumerator();
        }

        private void ResetLayerIndexes(int from, int to)
        {
            for (int i = from; i < to; i += 1)
            {
                Layer layer = _layers[i];
                _layerIndexByLayer[layer] = i;
            }
        }
    }
}
