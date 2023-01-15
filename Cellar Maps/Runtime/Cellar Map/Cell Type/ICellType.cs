namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс типа клетки клеточной карты.
    /// </summary>
    public interface ICellType : IReadOnlyCellType
    {
        /// <summary>
        /// Инициализирует название типа клетки.
        /// </summary>
        /// <param name="name">Название типа клетки. Должно быть отличным от null, иначе 
        /// вызовет исключение.</param>
        public void SetName(string name);
    }
}
