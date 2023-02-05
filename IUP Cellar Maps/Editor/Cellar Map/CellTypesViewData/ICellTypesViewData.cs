using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Интерфейс визуальных данных палитры клеточной карты.
    /// </summary>
    public interface ICellTypesViewData : IReadOnlyList<ICellTypeViewData>
    {
        /// <summary>
        /// Количество визуальных данных типов клеток.
        /// </summary>
        public new int Count { get; }

        /// <summary>
        /// Индексатор для доступа к визуальным данным типа клетки.
        /// </summary>
        /// <param name="index">Индекс визуальных данных типа клетки.</param>
        /// <returns>Возвращает визуальные данные типа клетки.</returns>
        public new ICellTypeViewData this[int index] { get; }
        /// <summary>
        /// Индексатор для доступа к визуальным данным типа клетки по названию типа.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <returns>Возвращает визуальные данные типа клетки.</returns>
        public ICellTypeViewData this[string cellTypeName] { get; }

        /// <summary>
        /// Проверяет, содержит ли палитра визуальные данные для типа клетки с переданным названием типа.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <returns>Возвращает true, если палитра содержит визуальные данные для типа клетки с переданным 
        /// названием типа; иначе false.</returns>
        public bool Contains(string cellTypeName);

        /// <summary>
        /// Изменяет цвет типа клетки клеточной карты.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки клеточной карты.</param>
        /// <param name="cellTypeColor">Цвет типа клетки клеточной карты.</param>
        public void SetCellTypeColor(string cellTypeName, Color cellTypeColor);

        /// <summary>
        /// Перемещает визуальные данные типа клетки в палитре с одной позиции на другую, при этом сдвигая 
        /// все остальные элементы.
        /// </summary>
        /// <param name="from">Откуда переместить.</param>
        /// <param name="to">Куда переместить.</param>
        public void MoveItemFromTo(int from, int to);
    }
}
