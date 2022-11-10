using IUP_Toolkits.Matrices;
using UnityEngine;

namespace IUP_Toolkits.CellarMaps
{
    public interface ICellarMapLayer
    {
        /// <summary>
        /// Ширина слоя.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота слоя.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Индексатор для доступа к названиям типов клеток для дальнейшего сопоставления ключей 
        /// классами-генераторами.
        /// </summary>
        public IReadonlyMatrixIndexer<string> MappingKeys { get; }

        /// <summary>
        /// Индексатор для доступа к клетке по координатам.
        /// </summary>
        /// <param name="x">Координата клетки по оси x.</param>
        /// <param name="y">Координата клетки по оси y.</param>
        /// <returns>Возвращает ссылку на тип клетки по указанным координатам.</returns>
        public CellType this[int x, int y] { get; set; }
        /// <summary>
        /// Индексатор для доступа к клетке по координатам.
        /// </summary>
        /// <param name="coordinate">Координаты клетки.</param>
        /// <returns>Возвращает ссылку на тип клетки по указанным координатам.</returns>
        public CellType this[Vector2Int coordinate] { get; set; }

        /// <summary>
        /// Заполняет слой, присваивая всем клеткам значение ссылки на переданный тип клетки.
        /// </summary>
        /// <param name="type">Ссылка на тип клетки-заполнителя.</param>
        public void Fill(CellType type);

        /// <summary>
        /// Очищает слой, присваивая всем клеткам значение null.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Очищает слой от переданного типа клеток.
        /// </summary>
        /// <param name="removedType">Тип клетки, от которого необходимо очистить карту.</param>
        public void ClearFrom(CellType removedType);

        /// <summary>
        /// Заменяет один тип клеток на другой.
        /// </summary>
        /// <param name="replace">Заменяемый тип клетки.</param>
        /// <param name="other">Заменяющий тип клетки.</param>
        public void ReplaceWithOther(CellType replace, CellType other);
    }
}
