using System;
using IUP.Toolkits.Matrices;

namespace IUP.Toolkits.CellarMaps
{
    public interface ILayer : IReadOnlyLayer
    {
        /// <summary>
        /// Устанавливает название слоя.
        /// </summary>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        public void Rename(string layerName);

        /// <summary>
        /// Метод для доступа к клеткам по координатам: заглушка до добавления поддержки ковариантности 
        /// возвращаемых типов в Unity.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки клеточной карты.</param>
        /// <param name="y">Y-компонента координаты клетки клеточной карты.</param>
        /// <returns>Возвращает клетку слоя по указанным координатам.</returns>
        public Cell GetCell(int x, int y);

        /// <summary>
        /// Пересоздаёт слой, сбрасывая все значения клеток.
        /// </summary>
        /// <param name="width">Ширина слоя: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоя: должна быть больше или равна 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Recreate(int width, int height);

        /// <summary>
        /// Изменяет размер слоя в соответствии с переданными правилами.
        /// </summary>
        /// <param name="widthOffset">Величина изменения ширины слоя. После изменения размера ширина 
        /// слоя должна быть больше или равна 1.</param>
        /// <param name="heightOffset">Величина изменения высоты слоя. После изменения высота слоя 
        /// должна быть больше или равна 1.</param>
        /// <param name="widthResizeRule">Правило изменения ширины слоя.</param>
        /// <param name="heightResizeRule">Правило изменения высоты слоя.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Resize(
            int widthOffset,
            int heightOffset,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule);

        /// <summary>
        /// Поворачивает слой.
        /// </summary>
        /// <param name="matrixRotation">Тип вращения матрицы слоя.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Rotate(MatrixRotation matrixRotation);

        /// <summary>
        /// Отражает слой.
        /// </summary>
        /// <param name="matrixMirror">Тип отражения матрицы слоя.</param>
        public void Mirror(MatrixMirror matrixMirror);

        /// <summary>
        /// Очищает слой от переданного типа клетки, присваивая всем клеткам этого типа тип null и сбрасывая 
        /// уникальные данные.
        /// </summary>
        /// <param name="type">Тип клетки, от которого очищается слой.</param>
        public void ClearFrom(ICellType type);
    }
}
