using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс визуальных данных типа клетки клеточной карты. Предполагается к использованию только в редакторе.
    /// </summary>
    [Serializable]
    public sealed record CellTypeViewData
    {
        /// <summary>
        /// Название типа клетки клеточной карты. Предполагается к использованию только в редакторе.
        /// </summary>
        public string cell_type_name;
        /// <summary>
        /// Цвет типа клетки клеточной карты. Предполагается к использованию только в редакторе.
        /// </summary>
        public Color color;
    }
}
