using System;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс файла клеточной карты.
    /// </summary>
    [Serializable]
    public sealed record CellarMapFile
    {
        /// <summary>
        /// Версия формата данных.
        /// </summary>
        public int data_format_version;
        /// <summary>
        /// Визуальные данные клеточной карты.
        /// </summary>
        public CellarMapViewData cellar_map_view_data;
        /// <summary>
        /// Клеточная карта.
        /// </summary>
        public CellarMap cellar_map;
    }
}
