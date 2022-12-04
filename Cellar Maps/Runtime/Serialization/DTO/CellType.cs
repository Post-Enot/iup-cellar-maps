using System;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс типа клетки клеточной карты.
    /// </summary>
    [Serializable]
    public sealed record CellType
    {
        /// <summary>
        /// Название типа клетки клеточной карты.
        /// </summary>
        public string type_name;
    }
}
