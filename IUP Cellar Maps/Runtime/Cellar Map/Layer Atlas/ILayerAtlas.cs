namespace IUP.Toolkits.CellarMaps
{
    public interface ILayerAtlas : IReadOnlyLayerAtlas
    {
        /// <summary>
        /// Создаёт слой на основе переданных аргументов и помещает его в конец списка слоёв.
        /// </summary>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        /// <returns>Возвращает созданный слой клеточной карты.</returns>
        public void Add(string layerName);

        /// <summary>
        /// Удаляет слой по индексу.
        /// </summary>
        /// <param name="layerIndex">Индекс удаляемого слоя.</param>
        public void Remove(int layerIndex);

        /// <summary>
        /// Перемещает слой в списке.
        /// </summary>
        public void MoveLayerFromTo(int from, int to);

        /// <summary>
        /// Метод для доступа к слою по индексу: заглушка до добавления поддержки ковариантности возвращаемых 
        /// типов в Unity.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя.</param>
        /// <returns>Возвращает слой клеточной карты.</returns>
        public Layer GetLayer(int layerIndex);
    }
}
