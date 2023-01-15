using IUP.Toolkits.Matrices;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Интерфейс клеточной карты.
    /// </summary>
    public interface ICellarMap : IReadOnlyCellarMap
    {
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
        /// Поворачивает клеточную карту.
        /// </summary>
        /// <param name="matrixRotation">Тип вращения матриц слоёв клеточной карты.</param>
        public void Rotate(MatrixRotation matrixRotation);

        /// <summary>
        /// Отражает клеточную карту.
        /// </summary>
        /// <param name="matrixMirror">Тип отражения матриц слоёв клеточной карты.</param>
        public void Mirror(MatrixMirror matrixMirror);

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

        public void RenameCellType(string oldCellTypeName, string newCellTypeName);

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

        public void RenameLayer(int layerIndex, string newLayerName);

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
    }
}
