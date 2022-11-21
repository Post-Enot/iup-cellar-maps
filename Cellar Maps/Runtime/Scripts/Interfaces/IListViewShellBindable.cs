using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс, указывающий, что реализующий его класс возможно связать с представлением списка.
    /// </summary>
    /// <typeparam name="T">Тип ViewData.</typeparam>
    public interface IListViewShellBindable<T> where T : class
    {
        /// <summary>
        /// Добавляет элемент в список.
        /// </summary>
        public void Add();
        /// <summary>
        /// Удаляет элемент из списка по переданному индексу.
        /// </summary>
        /// <param name="itemIndex">Индекс удаляемого элемента.</param>
        public void Remove(int itemIndex);
        /// <summary>
        /// Перемещает элемент на позицию переданного индекса, передвигая все остальные элементы.
        /// </summary>
        /// <param name="from">Индекс передвигаемого элемента.</param>
        /// <param name="to">Индекс, на который передвигается элемент.</param>
        public void MoveItemFromTo(int from, int to);
        /// <summary>
        /// Обновляет выбранный элемент.
        /// </summary>
        /// <param name="selectedItemViewData">ViewData выбранного элемента.</param>
        public void UpdateSelectedItem(T selectedItemViewData);
        /// <summary>
        /// Возвращает копию списка элементов для его дальнейшей связки с представлением списка.
        /// </summary>
        /// <returns>Копия списка элементов.</returns>
        public List<T> GetBindableList();
    }
}
