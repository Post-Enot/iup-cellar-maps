using IUP.Toolkits.Matrices;
using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Слой клеточной карты.
    /// </summary>
    public sealed class Layer : ILayer
    {
        /// <summary>
        /// Инициализирует слой.
        /// </summary>
        /// <param name="width">Ширина слоя: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоя: должна быть больше или равна 1.</param>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        public Layer(int width, int height, string layerName)
        {
            _matrix = new Matrix<Cell>(width, height);
            _matrix.InitAllElements(() => new Cell());
            Rename(layerName);
        }

        public int Width => _matrix.Width;
        public int Height => _matrix.Height;
        public string Name { get; private set; }
        public IReadonlyMatrix<ICell> Matrix { get; }

        private readonly Matrix<Cell> _matrix;

        public ICell this[int x, int y] => _matrix[x, y];
        public ICell this[Vector2Int coordinate] => _matrix[coordinate];

        public void Rename(string layerName)
        {
            Name = layerName ?? throw new ArgumentNullException(nameof(layerName));
        }

        /// <summary>
        /// Метод для доступа к клеткам по координатам: заглушка до добавления поддержки ковариантности 
        /// возвращаемых типов в Unity.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки клеточной карты.</param>
        /// <param name="y">Y-компонента координаты клетки клеточной карты.</param>
        /// <returns>Возвращает клетку слоя по указанным координатам.</returns>
        public Cell GetCell(int x, int y)
        {
            return _matrix[x, y];
        }

        /// <summary>
        /// Пересоздаёт слой, сбрасывая все значения клеток.
        /// </summary>
        /// <param name="width">Ширина слоя: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоя: должна быть больше или равна 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Recreate(int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentException("Ширина слоя должна быть больше или равна 1.");
            }
            if (height < 1)
            {
                throw new ArgumentException("Высота слоя должна быть больше или равна 1.");
            }
            if (Width == width && Height == height)
            {
                _matrix.ForEachElements((ref Cell cell) => cell.Clear());
            }
            else
            {
                _matrix.Recreate(width, height);
                _matrix.InitAllElements(() => new Cell());
            }
        }

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
            HeightResizeRule heightResizeRule)
        {
            if ((Width + widthOffset) < 1)
            {
                throw new ArgumentException("Ширина слоя после изменения размера должна быть больше или равна 1.");
            }
            if ((Height + heightOffset) < 1)
            {
                throw new ArgumentException("Высота слоя после изменения размера должна быть больше или равна 1.");
            }
            _matrix.Resize(widthOffset, heightOffset, widthResizeRule, heightResizeRule);
            _matrix.ForEachElements(
                (ref Cell cell) => cell ??= new Cell());
        }

        /// <summary>
        /// Поворачивает слой.
        /// </summary>
        /// <param name="matrixRotation">Тип вращения матрицы слоя.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Rotate(MatrixRotation matrixRotation)
        {
            _matrix.Rotate(matrixRotation);
        }

        /// <summary>
        /// Отражает слой.
        /// </summary>
        /// <param name="matrixMirror">Тип отражения матрицы слоя.</param>
        public void Mirror(MatrixMirror matrixMirror)
        {
            _matrix.Mirror(matrixMirror);
        }

        /// <summary>
        /// Присваивает всем клеткам переданный тип клетки.
        /// </summary>
        /// <param name="type">Тип клетки-заполнитель.</param>
        public void Fill(ICellType type)
        {
            _matrix.ForEachElements(
                delegate (ref Cell cell)
                {
                    cell.SetCellType(type);
                });
        }

        /// <summary>
        /// Заменяет тип клетки всех клеток одного типа на другой тип и сбрасывает уникальные данные клеток.
        /// </summary>
        /// <param name="replace">Заменяемый тип клетки.</param>
        /// <param name="other">Заменяющий тип клетки.</param>
        public void ReplaceWithOther(ICellType replace, ICellType other)
        {
            _matrix.ForEachElements(
                delegate (ref Cell cell)
                {
                    if (cell.CellType == replace)
                    {
                        cell.SetCellType(other);
                    }
                });
        }

        /// <summary>
        /// Очищает слой, присваивая всем клеткам тип null и сбрасывая уникальные данные.
        /// </summary>
        public void Clear()
        {
            _matrix.ForEachElements(
                delegate (ref Cell cell)
                {
                    cell.Clear();
                });
        }

        /// <summary>
        /// Очищает слой от переданного типа клетки, присваивая всем клеткам этого типа тип null и сбрасывая 
        /// уникальные данные.
        /// </summary>
        /// <param name="type">Тип клетки, от которого очищается слой.</param>
        public void ClearFrom(ICellType type)
        {
            _matrix.ForEachElements(
                delegate (ref Cell cell)
                {
                    if (cell.CellType == type)
                    {
                        cell.Clear();
                    }
                });
        }
    }
}
