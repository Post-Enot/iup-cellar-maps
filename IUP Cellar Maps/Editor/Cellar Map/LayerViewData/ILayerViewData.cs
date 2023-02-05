using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Интерфейс визуальных данных слоя клеточной карты.
    /// </summary>
    public interface ILayerViewData
    {
        /// <summary>
        /// Название слоя клеточной карты.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Цвет слоя клеточной карты.
        /// </summary>
        public Color Color { get; set; }
    }
}
