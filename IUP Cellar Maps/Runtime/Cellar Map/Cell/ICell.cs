namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс клетки клеточной карты.
    /// </summary>
    public interface ICell : IReadOnlyCell
    {
        /// <summary>
        /// Очищает клетку, сбрасывая метаданные и устанавливая тип клетки равным null.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Изменяет тип клетки и сбрасывает метаданные.
        /// </summary>
        /// <param name="cellType">Тип клетки.</param>
        public void SetType(IReadOnlyCellType type);
    }
}
