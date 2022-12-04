using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс палитры, содержащей набор уникальных типов клеток клеточных карт.
    /// </summary>
    public interface IPalette : IReadOnlyCollection<ICellType>
    {
        /// <summary>
        /// Количество типов клеток в палитре.
        /// </summary>
        public new int Count { get; }

        /// <summary>
        /// Индексатор для доступа к типу клетки по названию типа.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <returns>Возвращает тип клетки.</returns>
        public ICellType this[string cellTypeName] { get; }

        /// <summary>
        /// Проверяет, содержит ли палитра тип клетки с переданным названием типа.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <returns>Возвращает true, если палитра содержит тип клетки с переданным названием типа; 
        /// иначе false.</returns>
        public bool Contains(string cellTypeName);

        /// <summary>
        /// Изменяет название типа клетки, если это возможно.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <param name="newTypeName">Новое название типа клетки.</param>
        /// <returns>Возвращает true, если название типа клетки было успешно изменено; иначе false.</returns>
        public bool RenameCellType(string cellTypeName, string newTypeName);
    }
}
