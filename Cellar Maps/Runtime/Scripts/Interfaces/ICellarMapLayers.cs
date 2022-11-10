using System.Collections.Generic;

namespace IUP_Toolkits.CellarMaps
{
    public interface ICellarMapLayers
    {
        /// <summary>
        /// Readonly-коллекция всех слоёв.
        /// </summary>
        public IReadOnlyList<ICellarMapLayer> Layers { get; }
        /// <summary>
        /// Ширина слоёв.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота слоёв.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Количество слоёв.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Индексатор для доступа к слоям.
        /// </summary>
        /// <param name="index">Индекс слоя.</param>
        /// <returns></returns>
        public ICellarMapLayer this[int index] { get; }

        /// <summary>
        /// Заполняет все, присваивая всем клеткам значение ссылки на переданный тип клетки.
        /// </summary>
        /// <param name="type">Ссылка на тип клетки-заполнителя.</param>
        public void FillAllLayers(CellType type);

        /// <summary>
        /// Очищает все слои, присваивая всем клеткам значение null.
        /// </summary>
        public void ClearAllLayers();

        /// <summary>
        /// Очищает все слои от переданного типа клеток.
        /// </summary>
        /// <param name="removedType">Тип клетки, от которого необходимо очистить все слои.</param>
        public void ClearAllLayersFrom(CellType removedType);

        /// <summary>
        /// Заменяет один тип клеток на другой на всех слоях.
        /// </summary>
        /// <param name="replace">Заменяемый тип клетки.</param>
        /// <param name="other">Заменяющий тип клетки.</param>
        public void ReplaceWithOtherInAllLayers(CellType replace, CellType other);

        /// <summary>
        /// Добавляет пустой слой.
        /// </summary>
        public void AddLayer();

        /// <summary>
        /// Удаляет слой по индексу.
        /// </summary>
        /// <param name="index">Индекс удаляемого слоя.</param>
        public void RemoveLayer(int index);

        /// <summary>
        /// Перемещает слой на позицию переданного индекса, передвигая все остальные слои.
        /// </summary>
        /// <param name="from">Индекс передвигаемого слоя.</param>
        /// <param name="to">Индекс, на который передвигается слой.</param>
        public void MoveLayerFromTo(int from, int to);
    }
}
