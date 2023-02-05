using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления клетки клеточной карты.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Координаты клетки на клеточной карте.
        /// </summary>
        public Vector2Int Coordinate { get; }

        /// <summary>
        /// Вызывается при нажатии на клетку: передаёт собственные координаты на клеточной карте.
        /// </summary>
        public event Action<Vector2Int> Clicked;

        /// <summary>
        /// Обновляет представление клетки.
        /// </summary>
        /// <param name="viewData">Визуальные данные клетки.</param>
        public void UpdateView(CellViewData viewData);
    }
}
