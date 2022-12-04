using System;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс слоя клеточной карты.
    /// </summary>
    [Serializable]
    public sealed record Layer
    {
        /// <summary>
        /// Название слоя клеточной карты.
        /// </summary>
        public string layer_name;
        /// <summary>
        /// Массив всех клеток слоя клеточной карты.
        /// </summary>
        public Cell[] cells;
    }
}
