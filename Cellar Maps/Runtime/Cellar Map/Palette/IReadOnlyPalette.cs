using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// ReadOnly-интерфейс палитры, содержащей набор уникальных типов клеток клеточных карт.
    /// </summary>
    public interface IReadOnlyPalette : IReadOnlyCollection<ICellType>
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
        public IReadOnlyCellType this[string cellTypeName] { get; }

        /// <summary>
        /// Проверяет, содержит ли палитра тип клетки с переданным названием типа.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <returns>Возвращает true, если палитра содержит тип клетки с переданным названием типа; 
        /// иначе false.</returns>
        public bool Contains(string cellTypeName);

        /// <returns>Возвращает перечислитель типов клеток.</returns>
        public new IEnumerator<ICellType> GetEnumerator();
    }
}
