using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public interface IPalette
    {
        /// <summary>
        /// Вызывается при добавлении элемента в палитру.
        /// </summary>
        public event Action<int> Added;
        /// <summary>
        /// Вызывается при удалении элемента из палитры.
        /// </summary>
        public event Action<int> Removed;
        /// <summary>
        /// Вызывается при перемещении типа клетки в палитре. Первый аргумент - индекс старой позиции; 
        /// второй аргумент - индекс новой позиции.
        /// </summary>
        public event Action<int, int> MovedFromTo;
        public event Action<string, string, Color> ViewDataChanged;
        /// <summary>
        /// Вызывается при выборе элемента: возвращает последний выбранный элемент.
        /// </summary>
        public event Action<ICellTypeViewData> Selected;
        public event Action Unselected;

        /// <summary>
        /// Инициализирует список элементов.
        /// </summary>
        /// <param name="viewDataList">Список визуальных данных типов клеток.</param>
        public void BindWith(IReadOnlyList<ICellTypeViewData> viewDataList);
    }
}
