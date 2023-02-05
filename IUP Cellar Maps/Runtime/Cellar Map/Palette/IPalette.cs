namespace IUP.Toolkits.CellarMaps
{
    public interface IPalette : IReadOnlyPalette
    {
        /// <summary>
        /// Создаёт новый тип клетки и добавляет его в палитру.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки. Должно быть уникальным, иначе вызовет 
        /// исключение ArgumentException.</param>
        /// <returns>Возвращает ссылку на созданный тип клетки.</returns>
        public void Add(string cellTypeName);

        /// <summary>
        /// Удаляет передаванный тип клетки из палитры.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        public void Remove(string cellTypeName);

        /// <summary>
        /// Изменяет название типа клетки, если это возможно.
        /// </summary>
        /// <param name="oldCellTypeName">Старое название типа клетки.</param>
        /// <param name="newCellTypeName">Новое название типа клетки.</param>
        /// <returns>Возвращает true, если название типа клетки было успешно изменено; иначе false.</returns>
        public void RenameCellType(string oldCellTypeName, string newCellTypeName);
    }
}
