namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс клетки клеточной карты.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Тип клетки клеточной карты.
        /// </summary>
        public ICellType CellType { get; }
        /// <summary>
        /// Ключ сопоставления клетки клеточной карты, предполагаемый к использованию классами-генераторами 
        /// для идентификации типа клетки клеточной карты. Если клетка пуста, равен null.
        /// </summary>
        public string MappingKey { get; }
        /// <summary>
        /// Уникальные данные клетки клеточной карты, предполагаемые к использованию классами-генераторами. 
        /// Может иметь абсолютно любое значение.
        /// </summary>
        public string UniqueData { get; }
        /// <summary>
        /// Клетка считается пустой, если её тип равен null и в ней отсутствуют уникальные данные.
        /// </summary>
        public bool IsEmpty { get; }
        /// <summary>
        /// True, если клетка имеет уникальные данные.
        /// </summary>
        public bool HasUniqueData { get; }
        /// <summary>
        /// True, если клетка имеет тип.
        /// </summary>
        public bool HasCellType { get; }
    }
}
