namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// ReadOnly-интерфейс типа клетки клеточной карты.
    /// </summary>
    public interface IReadOnlyCellType
    {
        /// <summary>
        /// Название типа клетки. Значение никогда не равно null.
        /// </summary>
        public string Name { get; }
    }
}
