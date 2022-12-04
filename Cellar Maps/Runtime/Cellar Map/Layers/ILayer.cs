using IUP.Toolkits.Matrices;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс слоя клеточной карты.
    /// </summary>
    public interface ILayer
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
        public IReadonlyMatrix<ICell> Matrix { get; }

        /// <summary>
        /// Индексатор для доступа к клеткам слоя клеточной карты.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки клеточной карты.</param>
        /// <param name="y">Y-компонента координаты клетки клеточной карты.</param>
        /// <returns>Возвращает клетку слоя по указанным координатам. Возвращаемое значение никогда не равно 
        /// null.</returns>
        public ICell this[int x, int y] { get; }
        /// <summary>
        /// Индексатор для доступа к клеткам слоя клеточной карты.
        /// </summary>
        /// <param name="coordinate">Координаты клетки клеточной карты.</param>
        /// <returns>Возвращает клетку слоя по указанным координатам. Возвращаемое значение никогда не равно 
        /// null.</returns>
        public ICell this[Vector2Int coordinate] { get; }

        /// <summary>
        /// Устанавливает название слоя.
        /// </summary>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        public void Rename(string layerName);
    }
}
