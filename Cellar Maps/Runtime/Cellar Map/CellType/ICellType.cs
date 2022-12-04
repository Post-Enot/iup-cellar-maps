namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс типа клетки клеточной карты.
    /// </summary>
    public interface ICellType
    {
        /// <summary>
        /// Название типа клетки. Значение никогда не равно null.
        /// </summary>
        public string TypeName { get; }
    }
}
