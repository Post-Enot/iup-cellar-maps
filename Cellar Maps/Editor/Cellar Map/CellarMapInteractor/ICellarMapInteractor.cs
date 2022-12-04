using IUP.Toolkits.Matrices;
using System;
using UnityEngine;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps.Editor
{
    /// <summary>
    /// Интерфейс варианта использования клеточной карты.
    /// </summary>
    public interface ICellarMapInteractor
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
        /// Визуальные данные типов клеток клеточной карты.
        /// </summary>
        public ICellTypesViewData CellTypesViewData { get; }
        /// <summary>
        /// Визуальные данные слоёв клеточной карты.
        /// </summary>
        public ILayersViewData LayersViewData { get; }
        public IPalette Palette { get; }

        /// <summary>
        /// Индексатор для доступа к слоям клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <returns>Возвращает слой клеточной карты.</returns>
        public ILayer this[int layerIndex] { get; }

        public void SetMarkUnsavedChangesCallback(Action markUnsavedCallback);

        /// <summary>
        /// Пересоздаёт клеточную карту, очищая все клетки.
        /// </summary>
        /// <param name="width">Ширина клеточной карты.</param>
        /// <param name="height">Высота клеточной карты.</param>
        public void Recreate(int width, int height);

        /// <summary>
        /// Изменяет размер клеточной карты без потери данных.
        /// </summary>
        /// <param name="width">Ширина клеточной карты.</param>
        /// <param name="height">Высота клеточной карты.</param>
        /// <param name="widthResizeRule">Правило изменения ширины клеточной карты.</param>
        /// <param name="heightResizeRule">Правило изменения высоты клеточной карты.</param>
        public void Resize(
            int width,
            int height,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule);

        /// <summary>
        /// Возвращает первую не пустую или самую нижнюю клеткую при переборе сверху вниз.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <param name="startLayerIndex">Индекс слоя, с которого начинается перебор.</param>
        /// <returns>Возвращает кортеж из двух элементов: ссылки первую не пустую или самую 
        /// нижнюю клеткую при переборе сверху вниз (cell) и индекс слоя клетки (cellLayerIndex).</returns>
        public (ICell cell, int cellLayerIndex) GetTopCell(int x, int y, int startLayerIndex = 0);

        /// <summary>
        /// Возвращает первую не пустую или самую нижнюю клеткую при переборе сверху вниз.
        /// </summary>
        /// <param name="coordinate">Координаты клетки.</param>
        /// <param name="startLayerIndex">Индекс слоя, с которого начинается перебор.</param>
        /// <returns>Возвращает кортеж из двух элементов: ссылки первую не пустую или самую 
        /// нижнюю клеткую при переборе сверху вниз (cell) и индекс слоя клетки (cellLayerIndex).</returns>
        public (ICell cell, int cellLayerIndex) GetTopCell(Vector2Int coordinate, int startLayerIndex = 0);

        /// <summary>
        /// Добавляет новый тип клетки в палитру клеточной карты и создаёт визуальные данные для неё.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <param name="cellTypeColor">Цвет типа клетки.</param>
        public void AddCellTypeToPalette(string cellTypeName, Color cellTypeColor);
        /// <summary>
        /// Удаляет тип клетки из палитры клеточной карты и связанные с ней визуальные данные; очищает 
        /// клеточную карту от всех клеток удалённого типа.
        /// </summary>
        /// <param name="cellTypeName">Название удаляемого типа клетки.</param>
        public void RemoveCellTypeFromPalette(string cellTypeName);

        /// <summary>
        /// Изменяет название типа клетки, если это возможно.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки.</param>
        /// <param name="newCellTypeName">Новое название типа клетки.</param>
        /// <returns>Возвращает true, если название типа клетки было успешно изменено; 
        /// иначе false.</returns>
        public bool RenameCellType(string cellTypeName, string newCellTypeName);

        /// <summary>
        /// Изменяет цвет типа клетки клеточной карты.
        /// </summary>
        /// <param name="cellTypeName">Название типа клетки клеточной карты.</param>
        /// <param name="cellTypeColor">Цвет типа клетки клеточной карты.</param>
        public void SetCellTypeColor(string cellTypeName, Color cellTypeColor);

        /// <summary>
        /// Добавляет новый слой в клеточную карту и создаёт визуальные данные для него.
        /// </summary>
        /// <param name="layerName">Название слоя.</param>
        /// <param name="layerColor">Цвет слоя.</param>
        public void AddLayer(string layerName, Color layerColor);

        /// <summary>
        /// Удаляет по индексу слой клеточной карты и связанные с ним визуальные данные.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        public void RemoveLayer(int layerIndex);

        /// <summary>
        /// Изменяет название слоя по индексу, если это возможно.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя.</param>
        /// <param name="newLayerName">Новое название слоя.</param>
        /// <returns>Возвращает true, если название слоя было успешно изменено; 
        /// иначе false.</returns>
        public void RenameLayer(int layerIndex, string newLayerName);

        /// <summary>
        /// Изменяет цвет слоя клеточной карты.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клеточной карты.</param>
        /// <param name="layerColor">Цвет слоя клеточной карты.</param>
        public void SetLayerColor(int layerIndex, Color layerColor);

        /// <summary>
        /// Изменяет тип клетки и сбрасывает уникальные данные.
        /// </summary>
        /// <param name="layerIndex">Индекс слоя клетки.</param>
        /// <param name="x">X-компонента координаты клетки.</param>
        /// <param name="y">Y-компонента координаты клетки.</param>
        /// <param name="cellTypeName">Название типа клетки.</param>
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

        public (DTO.CellarMapViewData, DTO.CellarMap) ToDTO();
    }
}
