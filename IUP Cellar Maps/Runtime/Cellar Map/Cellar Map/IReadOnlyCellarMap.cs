namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// ReadOnly-интерфейс клеточной карты.
    /// </summary>
    public interface IReadOnlyCellarMap
    {
        /// <summary>
        /// Ширина клеточной карты.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота клеточной карты.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Количество слоёв клеточной карты.
        /// </summary>
        public int LayersCount { get; }
        /// <summary>
        /// Палитра типов клеток клеточной карты.
        /// </summary>
        public IReadOnlyPalette Palette { get; }

        /// <summary>
        /// Индексатор для доступа к слоям клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <returns>Возвращает слой клеточной карты.</returns>
        public IReadOnlyLayer this[int layerIndex] { get; }
    }
}
