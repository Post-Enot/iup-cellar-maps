using IUP.Toolkits.Matrices;
using UnityEngine;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс клеточной карты.
    /// </summary>
    public interface ICellarMap
    {
        /// <summary>
        /// Ширина клеточной карты.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Высота клеточной карты.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// Количество слоёв клеточной карты.
        /// </summary>
        public int LayersCount { get; }
        /// <summary>
        /// Палитра типов клеток клеточной карты.
        /// </summary>
        public IPalette Palette { get; }

        /// <summary>
        /// Индексатор для доступа к слоям клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <returns>Возвращает слой клеточной карты.</returns>
        public ILayer this[int layerIndex] { get; }

        /// <summary>
        /// Пересоздаёт клеточную карту, очищая все клетки.
        /// </summary>
        /// <param name="width">Ширина клеточной карты.</param>
        /// <param name="height">Высота клеточной карты.</param>
        public void Recreate(int width, int height);

        /// <summary>
        /// Изменяет размер клеточной карты без потери данных.
        /// </summary>
        /// <param name="widthOffset">Ширина клеточной карты.</param>
        /// <param name="heightOffset">Высота клеточной карты.</param>
        /// <param name="widthResizeRule">Правило изменения ширины клеточной карты.</param>
        /// <param name="heightResizeRule">Правило изменения высоты клеточной карты.</param>
        public void Resize(
            int widthOffset,
            int heightOffset,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule);

        /// <summary>
        /// Создаёт новый тип клетки и добавляет его в палитру.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки. Должно быть уникальным, иначе вызовет 
        /// исключение ArgumentException.</param>
        /// <returns>Возвращает созданный тип клетки.</returns>
        public void AddCellTypeToPalette(string cellTypeName);

        /// <summary>
        /// Удаляет передаванный тип клетки из палитры и очищает клеточную карту от переданного типа.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        public void RemoveCellTypeFromPalette(string cellTypeName);

        /// <summary>
        /// Создаёт новый слой и добавляет его в клеточную карту.
        /// </summary>
        /// <param name="layerName">Название слоя: должно быть отличным от null, 
        /// иначе вызывает ArgumentNullException.</param>
        public void AddLayer(string layerName);

        /// <summary>
        /// Удаляет слой по индексу.
        /// </summary>
        /// <param name="layerIndex">Индекс удаляемого слоя.</param>
        public void RemoveLayer(int layerIndex);

        /// <summary>
        /// Изменяет тип клетки и сбрасывает уникальные данные.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клетки.</param>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <param name="cellTypeName">Тип клетки.</param>
        public void SetCellType(int layerIndex, int x, int y, string cellTypeName);

        /// <summary>
        /// Изменяет тип клетки и сбрасывает уникальные данные.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клетки.</param>
        /// <param name="coordinate">Координаты клетки.</param>
        /// <param name="cellTypeName">Название типа клетки.</param>
        public void SetCellType(int layerIndex, Vector2Int coordinate, string cellTypeName);

        /// <summary>
        /// Устанавливает уникальные данные клетки.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клетки.</param>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <param name="uniqueData">Уникальные данные.</param>
        public void SetCellUniqueData(int layerIndex, int x, int y, string uniqueData);

        /// <summary>
        /// Устанавливает уникальные данные клетки.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клетки.</param>
        /// <param name="coordinate">Координаты клетки.</param>
        /// <param name="uniqueData">Уникальные данные.</param>
        public void SetCellUniqueData(int layerIndex, Vector2Int coordinate, string uniqueData);

        public DTO.CellarMap ToDTO();
    }
}
