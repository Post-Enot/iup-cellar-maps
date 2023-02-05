namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Клетка клеточной карты.
    /// </summary>
    public sealed class Cell : ICell
    {
        /// <summary>
        /// Инициализирует тип и уникальные данные клетки.
        /// </summary>
        /// <param name="type">Тип клетки.</param>
        /// <param name="metadata">Метаданные.</param>
        public Cell(IReadOnlyCellType type = null, string metadata = null)
        {
            Type = type;
            Metadata = metadata;
        }

        public IReadOnlyCellType Type { get; private set; }
        public string MappingKey => Type?.Name;
        public string Metadata { get; set; }
        public bool IsEmpty => (Type == null) && (Metadata == null);
        public bool HasMetadata => Metadata != null;
        public bool HasType => Type != null;

        public void Clear()
        {
            Type = null;
            Metadata = null;
        }

        public void SetType(IReadOnlyCellType type)
        {
            Type = type;
            Metadata = null;
        }
    }
}
