using System;

namespace IUP.Toolkits.CellarMaps.Serialization.DTO
{
    /// <summary>
    /// DTO-класс визуальных данных клеточной карты. Предполагается к использованию только в редакторе.
    /// </summary>
    [Serializable]
    public sealed record CellarMapViewData
    {
        /// <summary>
        /// Визуальные данные типов клеток клеточной карты. Предполагается к использованию только в редакторе.
        /// </summary>
        public CellTypeViewData[] cell_types_view_data;
        /// <summary>
        /// Визуальные данные слоёв клеточной карты. Предполагается к использованию только в редакторе.
        /// </summary>
        public LayerViewData[] layers_view_data;
    }
}
