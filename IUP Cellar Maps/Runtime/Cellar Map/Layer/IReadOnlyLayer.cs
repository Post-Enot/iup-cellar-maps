using IUP.Toolkits.Matrices;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// ReadOnly-интерфейс слоя клеточной карты.
    /// </summary>
    public interface IReadOnlyLayer
    {
        /// <summary>
        /// Ширина слоя клеточной карты.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота слоя клеточной карты.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Название слоя клеточной карты.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Readonly-интерфейс матрицы слоя клеточной карты.
        /// </summary>
        public IReadOnlyMatrix<IReadOnlyCell> Matrix { get; }

        /// <summary>
        /// Индексатор для доступа к клеткам слоя клеточной карты.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки клеточной карты.</param>
        /// <param name="y">Y-компонента координаты клетки клеточной карты.</param>
        /// <returns>Возвращает клетку слоя по указанным координатам. Возвращаемое значение никогда не равно 
        /// null.</returns>
        public IReadOnlyCell this[int x, int y] { get; }

        /// <summary>
        /// Индексатор для доступа к клеткам слоя клеточной карты.
        /// </summary>
        /// <param name="coordinate">Координаты клетки клеточной карты.</param>
        /// <returns>Возвращает клетку слоя по указанным координатам. Возвращаемое значение никогда не равно 
        /// null.</returns>
        public IReadOnlyCell this[Vector2Int coordinate] { get; }
    }
}
