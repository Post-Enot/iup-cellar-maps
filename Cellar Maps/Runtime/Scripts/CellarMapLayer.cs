using IUP.Toolkits.Matrices;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    [Serializable]
    public sealed class CellarMapLayer : ISerializationCallbackReceiver, ICellarMapLayer
    {
        public CellarMapLayer(int width, int height)
        {
            Layer = new Matrix<CellType>(width, height);
        }

        public int Width => Layer.Width;
        public int Height => Layer.Height;
        public Matrix<CellType> Layer { get; private set; }

        /// <summary>
        /// Событие, уведомляющее об изменениях клеток на слое. Первый аргумент - сам слой, второй - 
        /// координаты изменённых клеток.
        /// </summary>
        public event Action CellsChanged;

        [SerializeField] private int _serializableWidth;
        [SerializeReference] private CellType[] _serializableLayer;

        public CellType this[Vector2Int coordinate]
        {
            get => Layer[coordinate];
            set => SetCellByCoordinate(value, coordinate.x, coordinate.y);
        }
        public CellType this[int x, int y]
        {
            get => Layer[x, y];
            set => SetCellByCoordinate(value, x, y);
        }

        /// <summary>
        /// Пересоздаёт слой, сбрасывая все значения.
        /// </summary>
        /// <param name="width">Ширина слоя: должна быть больше или равна 1.</param>
        /// <param name="height">Высота слоя: должна быть больше или равна 1.</param>
        /// <exception cref="ArgumentException"></exception>
        public void Recreate(int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentException("Ширина слоя должна быть больше или равна 1.");
            }
            if (height < 1)
            {
                throw new ArgumentException("Высота слоя должна быть больше или равна 1.");
            }
            Layer.Recreate(width, height);
        }

        public void Fill(CellType type)
        {
            for (int y = 0; y < Height; y += 1)
            {
                for (int x = 0; x < Width; x += 1)
                {
                    Layer[x, y] = type;
                }
            }
            CellsChanged?.Invoke();
        }

        public void ReplaceWithOther(CellType replace, CellType other)
        {
            var coordinates = new List<Vector2Int>();
            for (int y = 0; y < Height; y += 1)
            {
                for (int x = 0; x < Width; x += 1)
                {
                    if (Layer[x, y] == replace)
                    {
                        Layer[x, y] = other;
                    }
                }
            }
            CellsChanged?.Invoke();
        }

        public void Clear()
        {
            Fill(null);
        }

        public void ClearFrom(CellType removedType)
        {
            ReplaceWithOther(removedType, null);
        }

        private void SetCellByCoordinate(CellType type, int x, int y)
        {
            Layer[x, y] = type;
            CellsChanged?.Invoke();
        }

        public void OnBeforeSerialize()
        {
            _serializableWidth = Width;
            _serializableLayer = Layer.ToArray();
        }

        public void OnAfterDeserialize()
        {
            int layerHeight = _serializableLayer.Length / _serializableWidth;
            Layer = new Matrix<CellType>(_serializableWidth, layerHeight);
            for (int y = 0; y < layerHeight; y += 1)
            {
                for (int x = 0; x < _serializableWidth; x += 1)
                {
                    int i = (y * _serializableWidth) + x;
                    Layer[x, y] = _serializableLayer[i];
                }
            }
        }
    }
}
