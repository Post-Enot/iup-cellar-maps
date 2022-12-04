using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Визуальные данные слоя клеточной карты.
    /// </summary>
    public sealed class LayerViewData : ILayerViewData
    {
        /// <summary>
        /// Инициализирует визуальные данные слоя клеточной карты.
        /// </summary>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        /// <param name="layerColor">Цвет слоя.</param>
        public LayerViewData(string layerName, Color layerColor)
        {
            Name = layerName;
            Color = layerColor;
        }

        /// <summary>
        /// Название слоя клеточной карты.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Цвет слоя клеточной карты.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Устанавливает название слоя.
        /// </summary>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        public void Rename(string layerName)
        {
            Name = layerName ?? throw new ArgumentNullException(nameof(layerName));
        }
    }
}
