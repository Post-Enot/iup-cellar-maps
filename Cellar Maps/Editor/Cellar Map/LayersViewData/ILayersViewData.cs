using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Интерфейс визуальных данных слоёв клеточной карты.
    /// </summary>
    public interface ILayersViewData : IReadOnlyList<ILayerViewData>
    {
        /// <summary>
        /// Количество визуальных данных слоёв.
        /// </summary>
        public new int Count { get; }

        /// <summary>
        /// Индексатор для доступа к визуальным данным слоёв клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <returns>Возвращает визуальные данные слоя клеточной карты.</returns>
        public new ILayerViewData this[int layerIndex] { get; }
    }
}
