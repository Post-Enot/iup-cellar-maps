namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Клетка клеточной карты.
    /// </summary>
    public sealed class Cell : ICell
    {
        /// <summary>
        /// Инициализирует тип и уникальные данные клетки клеточной карты.
        /// </summary>
        /// <param name="cellType">Тип клетки клеточной карты.</param>
        /// <param name="uniqueData">Уникальные данные клетки клеточной карты.</param>
        public Cell(ICellType cellType = null, string uniqueData = null)
        {
            CellType = cellType;
            UniqueData = uniqueData;
        }

        public ICellType CellType { get; private set; }
        public string MappingKey => CellType?.TypeName;
        public string UniqueData { get; set; }
        public bool IsEmpty => (CellType == null) && (UniqueData == null);
        public bool HasUniqueData => UniqueData != null;
        public bool HasCellType => CellType != null;

        /// <summary>
        /// Очищает клетку, сбрасывая уникальные данные и устанавливая тип клетки null.
        /// </summary>
        public void Clear()
        {
            CellType = null;
            UniqueData = null;
        }

        /// <summary>
        /// Изменяет тип клетки и сбрасывает уникальные данные.
        /// </summary>
        /// <param name="cellType">Тип клетки.</param>
        public void SetCellType(ICellType cellType)
        {
            CellType = cellType;
            UniqueData = null;
        }
    }
}
