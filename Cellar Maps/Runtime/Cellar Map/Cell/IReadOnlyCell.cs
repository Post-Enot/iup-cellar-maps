namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// ReadOnly-интерфейс клетки клеточной карты.
    /// </summary>
    public interface IReadOnlyCell
    {
        /// <summary>
        /// Тип клетки клеточной карты.
        /// </summary>
        public IReadOnlyCellType Type { get; }
        /// <summary>
        /// Ключ сопоставления типа клетки, предполагаемый к использованию классами-генераторами 
        /// для идентификации типа клетки. Если клетка пуста, равен null.
        /// </summary>
        public string MappingKey { get; }
        /// <summary>
        /// Метаданные клетки.
        /// </summary>
        public string Metadata { get; }
        /// <summary>
        /// Клетка считается пустой, если её тип равен null и она не содержит метаданных.
        /// </summary>
        public bool IsEmpty { get; }
        /// <summary>
        /// True, если клетка содержит метаданные.
        /// </summary>
        public bool HasMetadata { get; }
        /// <summary>
        /// True, если клетка имеет тип.
        /// </summary>
        public bool HasType { get; }
    }
}
