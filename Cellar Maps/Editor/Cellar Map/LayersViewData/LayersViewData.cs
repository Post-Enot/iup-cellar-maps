using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Визуальные данные слоёв клеточной карты.
    /// </summary>
    public sealed class LayersViewData : ILayersViewData
    {
        public int Count => _viewData.Count;

        private readonly List<LayerViewData> _viewData = new();

        public ILayerViewData this[int layerIndex] => _viewData[layerIndex];

        /// <summary>
        /// Инициализирует визуальные данные слоя клеточной карты.
        /// </summary>
        /// <param name="layerName">Название слоя.</param>
        /// <param name="layerColor">Цвет слоя.</param>
        public void Add(string layerName, Color layerColor)
        {
            var viewData = new LayerViewData(layerName, layerColor);
            _viewData.Add(viewData);
        }

        /// <summary>
        /// Удаляет визуальные данные слоя клеточной карты по индексу.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        public void Remove(int layerIndex)
        {
            _viewData.RemoveAt(layerIndex);
        }

        public IEnumerator<ILayerViewData> GetEnumerator()
        {
            return _viewData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _viewData.GetEnumerator();
        }
    }
}
