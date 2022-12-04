using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс атласа слоёв клеточной карты.
    /// </summary>
    public interface ILayerAtlas : IReadOnlyList<ILayer>
    {
        /// <summary>
        /// Ширина слоёв клеточной карты.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота слоёв клеточной карты.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Количество слоёв клеточной карты.
        /// </summary>
        public new int Count { get; }
        /// <summary>
        /// Словарь для доступа к индексу слоя по самому слою.
        /// </summary>
        public IReadOnlyDictionary<ILayer, int> LayerIndexByLayer { get; }

        /// <summary>
        /// Индексатор для доступа к слоям клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <returns>Возвращает слой клеточной карты.</returns>
        public new ILayer this[int layerIndex] { get; }
    }
}
