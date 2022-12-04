using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс клетки клеточной карты.
    /// </summary>
    [Serializable]
    public sealed record Cell
    {
        /// <summary>
        /// Индекс типа клетки клеточной карты. Если клетка пуста, равен null.
        /// </summary>
        public int cell_type_index;
        /// <summary>
        /// Координата клетки клеточной карты.
        /// </summary>
        public Vector2Int coordinate;
        /// <summary>
        /// Уникальная информация клетки клеточной карты.
        /// </summary>
        public string unique_data;
    }
}
