using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Интерфейс визуальных данных типа клетки клеточной карты.
    /// </summary>
    public interface ICellTypeViewData
    {
        /// <summary>
        /// Название типа клетки.
        /// </summary>
        public string CellTypeName { get; }
        /// <summary>
        /// Цвет типа клетки.
        /// </summary>
        public Color Color { get; }
    }
}
