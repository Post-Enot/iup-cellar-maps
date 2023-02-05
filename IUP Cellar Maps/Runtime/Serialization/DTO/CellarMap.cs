using System;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс клеточной карты.
    /// </summary>
    [Serializable]
    public sealed record CellarMap
    {
        /// <summary>
        /// Ширина клеточной карты.
        /// </summary>
        public int width;
        /// <summary>
        /// Высота клеточной карты.
        /// </summary>
        public int height;
        /// <summary>
        /// Массив типов клеток клеточной карты.
        /// </summary>
        public CellType[] cell_types;
        /// <summary>
        /// Массив слоёв клеточной карты.
        /// </summary>
        public Layer[] layers;
    }
}
