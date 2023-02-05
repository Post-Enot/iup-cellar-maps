using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс визуальных данных слоя клеточной карты. Предполагается к использованию только в редакторе.
    /// </summary>
    [Serializable]
    public sealed record LayerViewData
    {
        /// <summary>
        /// Название слоя клеточной карты.
        /// </summary>
        public string layer_name;

        /// <summary>
        /// Цвет слоя клеточной карты.
        /// </summary>
        public Color color;
    }
}
