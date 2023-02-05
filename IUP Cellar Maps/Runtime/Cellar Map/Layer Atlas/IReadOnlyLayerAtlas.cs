using System.Collections.Generic;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// ReadOnly-интерфейс атласа слоёв клеточной карты.
    /// </summary>
    public interface IReadOnlyLayerAtlas : IReadOnlyList<IReadOnlyLayer>
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
        public IReadOnlyDictionary<IReadOnlyLayer, int> LayerIndexByLayer { get; }

        /// <summary>
        /// Индексатор для доступа к слоям клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <returns>Возвращает слой клеточной карты.</returns>
        public new IReadOnlyLayer this[int layerIndex] { get; }
    }
}
