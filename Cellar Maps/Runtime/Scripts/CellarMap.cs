using System;
using UnityEngine;

namespace IUP_Toolkits.CellarMaps
{
    /// <summary>
    /// Представляет тип карты, состоящего из клеток.
    /// </summary>
    [Serializable]
    public sealed class CellarMap : ISerializationCallbackReceiver
    {
        /// <summary>
        /// Инициализирует карту.
        /// </summary>
        /// <param name="width">Ширина карты: должна быть больше или равна 1.</param>
        /// <param name="height">Высота карты: должна быть больше или равна 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public CellarMap(int width, int height, Palette palette)
        {
            _layers = new CellarMapLayers(width, height);
            _layers.CellsChanged += HandleCellsChangingOnLayers;
            TopCells = new TopCellIndexer(_layers);
            _palette = palette;
            Palette.CellTypeRemovedFromPalette += _layers.ClearAllLayersFrom;
        }

        /// <summary>
        /// Ширина слоёв карты.
        /// </summary>
        public int Width => _layers.Width;
        /// <summary>
        /// Высота слоёв карты.
        /// </summary>
        public int Height => _layers.Height;
        /// <summary>
        /// Палитра, клетки из которой используются для заполнения слоёв карты.
        /// </summary>
        public Palette Palette => _palette;
        public ICellarMapLayers Layers => _layers;
        public TopCellIndexer TopCells { get; private set; }

        /// <summary>
        /// Вызывается при изменении значения клеток.
        /// </summary>
        public event Action CellsChanged;
        public event Action Recreated;

        [SerializeReference] private Palette _palette;
        [SerializeReference] private CellarMapLayers _layers;

        public ICellarMapLayer this[int layerIndex] => _layers[layerIndex];

        /// <summary>
        /// Пересоздаёт карту, сбрасывая все значения.
        /// </summary>
        /// <param name="width">Ширина карты: должна быть больше или равна 1.</param>
        /// <param name="height">Высота карты: должна быть больше или равна 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Recreate(int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentException("Ширина поля должна быть больше или равна 1.");
            }
            if (height < 1)
            {
                throw new ArgumentException("Высота поля должна быть больше или равна 1.");
            }
            _layers.RecreateAllLayers(width, height);
            Recreated?.Invoke();
        }

        private void HandleCellsChangingOnLayers()
        {
            CellsChanged?.Invoke();
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            Palette.CellTypeRemovedFromPalette += _layers.ClearAllLayersFrom;
            TopCells = new TopCellIndexer(_layers);
            _layers.CellsChanged += HandleCellsChangingOnLayers;
        }
    }
}
