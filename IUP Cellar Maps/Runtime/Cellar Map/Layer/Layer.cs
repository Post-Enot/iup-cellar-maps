using System;
using IUP.Toolkits.Matrices;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Слой клеточной карты.
    /// </summary>
    public sealed class Layer : IReadOnlyLayer
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
        public IReadOnlyMatrix<IReadOnlyCell> Matrix { get; }

        private readonly Matrix<Cell> _matrix;

        public IReadOnlyCell this[int x, int y] => _matrix[x, y];

        public IReadOnlyCell this[Vector2Int coordinate] => _matrix[coordinate];

        public void Rename(string layerName)
        {
            Name = layerName ?? throw new ArgumentNullException(nameof(layerName));
        }

        public Cell GetCell(int x, int y)
        {
            return _matrix[x, y];
        }

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
            if ((Width == width) && (Height == height))
            {
                _matrix.ForEachElements((ref Cell cell) => cell.Clear());
            }
            else
            {
                _matrix.Recreate(width, height);
                _matrix.InitAllElements(() => new Cell());
            }
        }

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
            _matrix.ForEachElements((ref Cell cell) => cell ??= new Cell());
        }

        public void Rotate(MatrixRotation matrixRotation)
        {
            _matrix.Rotate(matrixRotation);
        }

        public void Mirror(MatrixMirror matrixMirror)
        {
            _matrix.Mirror(matrixMirror);
        }

        public void ClearFrom(IReadOnlyCellType type)
        {
            _matrix.ForEachElements(
                (ref Cell cell) =>
                {
                    if (cell.Type == type)
                    {
                        cell.Clear();
                    }
                });
        }
    }
}
