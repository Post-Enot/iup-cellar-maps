using System;
using System.Collections.Generic;
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
            _palette = palette;
            Palette.CellTypeRemovedFromPalette += ClearFrom;
            Recreate(width, height);
        }

        ~CellarMap()
        {
            CellsChanged = null;
        }

        /// <summary>
        /// Ширина карты.
        /// </summary>
        public int Width => _map.GetLength(1);
        /// <summary>
        /// Высота карты.
        /// </summary>
        public int Height => _map.GetLength(0);
        /// <summary>
        /// Палитра, клетки из которой используются для заполнения карты.
        /// </summary>
        public Palette Palette => _palette;

        /// <summary>
        /// Вызывается при изменении значения клеток; передаёт в качестве аргумента массив со списком координат 
        /// изменённых клеток.
        /// </summary>
        public event Action<Vector2Int[]> CellsChanged;
        public event Action Recreated;

        [SerializeField] private CellType[,] _map;
        [SerializeField] private int _smapWidth;
        [SerializeField][SerializeReference] private Palette _palette;
        [SerializeField][SerializeReference] private CellType[] _smap;

        /// <summary>
        /// Индексатор для доступа к клетке по координатам.
        /// </summary>
        /// <param name="x">Координата клетки по оси x.</param>
        /// <param name="y">Координата клетки по оси y.</param>
        /// <returns>Возвращает ссылку на тип клетки по указанным координатам.</returns>
        public CellType this[int x, int y]
        {
            get => _map[y, x];
            set => SetCellByCoordinate(value, x, y);
        }
        /// <summary>
        /// Индексатор для доступа к клетке по координатам.
        /// </summary>
        /// <param name="coordinate">Координаты клетки.</param>
        /// <returns>Возвращает ссылку на тип клетки по указанным координатам.</returns>
        public CellType this[Vector2Int coordinate]
        {
            get => _map[coordinate.y, coordinate.x];
            set => SetCellByCoordinate(value, coordinate.x, coordinate.y);
        }

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
            _map = new CellType[height, width];
            Recreated?.Invoke();
        }

        /// <summary>
        /// Очищает карту, присваивая всем клеткам значение null.
        /// </summary>
        public void Clear()
        {
            Fill(null);
        }

        /// <summary>
        /// Заполняет карту, присваивая всем клеткам значение ссылки на переданный тип клетки.
        /// </summary>
        /// <param name="type">Ссылка на тип клетки-заполнителя.</param>
        public void Fill(CellType type)
        {
            var coordinates = new List<Vector2Int>();
            for (int y = 0; y < Height; y += 1)
            {
                for (int x = 0; x < Width; x += 1)
                {
                    if (_map[y, x] != type)
                    {
                        _map[y, x] = type;
                        coordinates.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Очищает карту от переданного типа клеток.
        /// </summary>
        /// <param name="removedType">Тип клетки, от которого необходимо очистить карту.</param>
        public void ClearFrom(CellType removedType)
        {
            ReplaceWithOthers(removedType, null);
        }

        /// <summary>
        /// Заменяет один тип клеток на другой.
        /// </summary>
        /// <param name="replace">Заменяемый тип клетки.</param>
        /// <param name="other">Заменяющий тип клетки.</param>
        private void ReplaceWithOthers(CellType replace, CellType other)
        {
            var coordinates = new List<Vector2Int>();
            for (int y = 0; y < Height; y += 1)
            {
                for (int x = 0; x < Width; x += 1)
                {
                    if (_map[y, x] == replace)
                    {
                        _map[y, x] = other;
                        coordinates.Add(new Vector2Int(x, y));
                    }
                }
            }
            CellsChanged?.Invoke(coordinates.ToArray());
        }

        private void SetCellByCoordinate(CellType type, int x, int y)
        {
            if (_map[y, x] != type)
            {
                _map[y, x] = type;
                var coordinates = new Vector2Int[1] { new(x, y) };
                CellsChanged?.Invoke(coordinates);
            }
        }

        public void OnBeforeSerialize()
        {
            _smap = new CellType[Width * Height];
            _smapWidth = Width;
            for (int i = 0; i < _smap.Length; i += 1)
            {
                int x = i % Width;
                int y = i / Width;
               _smap[i] = _map[y, x];
            }
        }
        
        public void OnAfterDeserialize()
        {
            int height = _smap.Length / _smapWidth;
            _map = new CellType[height, _smapWidth];
            for (int y = 0; y < height; y += 1)
            {
                for (int x = 0; x < _smapWidth; x += 1)
                {
                    int i = (y * _smapWidth) + x;
                    _map[y, x] = _smap[i];
                }
            }
            Palette.CellTypeRemovedFromPalette += ClearFrom;
        }
    }
}
