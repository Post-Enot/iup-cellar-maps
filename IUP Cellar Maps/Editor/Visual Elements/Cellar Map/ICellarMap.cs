using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления клеточной карты.
    /// </summary>
    public interface ICellarMap
    {
        /// <summary>
        /// Ширина клеточной карты.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота клеточной карты.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Вызывается при нажатии на клетку клеточной карты: возвращает координаты нажатой клетки.
        /// </summary>
        public event Action<Vector2Int> Clicked;

        /// <summary>
        /// Индексатор для доступа к представлению клеток клеточной карты.
        /// </summary>
        /// <param name="x">X-компонента координаты представления клетки.</param>
        /// <param name="y">Y-компонента координаты представления клетки.</param>
        /// <returns>Возвращает представление клетки.</returns>
        public ICell this[int x, int y] { get; }
        /// <summary>
        /// Индексатор для доступа к представлению клеток клеточной карты.
        /// </summary>
        /// <param name="coordinate">Координаты представления клетки.</param>
        /// <returns>Возвращает представление клетки.</returns>
        public ICell this[Vector2Int coordinate] { get; }

        /// <summary>
        /// Пересоздаёт представление клеточной карты.
        /// </summary>
        /// <param name="width">Ширина клеточной карты.</param>
        /// <param name="height">Высота клеточной карты.</param>
        public void Recreate(int width, int height);

        /// <summary>
        /// Обновляет визуальные данные клетки по переданным координатам.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <param name="viewData">Визуальные данные клетки.</param>
        public void UpdateCellViewData(int x, int y, CellViewData viewData);

        /// <summary>
        /// Обновляет визуальные данные клетки по переданным координатам.
        /// </summary>
        /// <param name="coordinate">Координаты клетки.</param>
        /// <param name="viewData">Визуальные данные клетки.</param>
        public void UpdateCellViewData(Vector2Int coordinate, CellViewData viewData);
    }
}
