using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент строки клеток клеточной карты.
    /// </summary>
    public class CellarMapRow : VisualElement
    {
        /// <summary>
        /// Инициализирует визуальный элемент строки клеточной карты.
        /// </summary>
        public CellarMapRow()
        {
            AddToClassList("cm-cellar-map-row");
        }

        /// <summary>
        /// Инициализирует визуальный элемент строки клеточной карты.
        /// </summary>
        /// <param name="rowIndex">Индекс строки клеточной карты.</param>
        /// <param name="width">Ширина строки.</param>
        public CellarMapRow(int rowIndex, int width = 0) : this()
        {
            Index = rowIndex;
            SetWidth(width);
        }

        /// <summary>
        /// Индекс строки клеточной карты.
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// Ширина строки клеточной карты.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Список представления клеток.
        /// </summary>
        public IReadOnlyList<ICell> Cells => _cells;

        private readonly List<Cell> _cells = new();

        /// <summary>
        /// Устанавливает индекс строки клеточной карты и обновляет координаты клеток.
        /// </summary>
        /// <param name="rowIndex">Индекс строки клеточной карты.</param>
        public void SetRowIndex(int rowIndex)
        {
            Index = rowIndex;
            for (int x = 0; x < _cells.Count; x += 1)
            {
                _cells[x].Coordinate = new Vector2Int(x, rowIndex);
            }
        }

        /// <summary>
        /// Устанавливает ширину строки клеточной карты.
        /// </summary>
        /// <param name="width">Ширина строки клеточной карты.</param>
        public void SetWidth(int width)
        {
            if (width < 0)
            {
                throw new ArgumentException(
                    "Ширина строки клеточной карты не может быть меньше 0.",
                    nameof(width));
            }
            for (int i = 0; i < _cells.Count; i += 1)
            {
                Remove(_cells[i]);
            }
            _cells.Clear();
            for (int x = 0; x < width; x += 1)
            {
                var cell = new Cell(Index, x);
                _cells.Add(cell);
                Add(cell);
            }
        }
    }
}
