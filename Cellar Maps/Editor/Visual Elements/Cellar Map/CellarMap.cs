using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент клеточной карты.
    /// </summary>
    public sealed class CellarMap : VisualElement, ICellarMap
    {
        /// <summary>
        /// Инициализирует визуальный элемент клеточной карты.
        /// </summary>
        public CellarMap()
        {
            AddToClassList("cm-cellar-map");
        }

        public int Width => _rows.Count > 0 ? _rows[0].Width : 0;
        public int Height => _rows.Count;

        public event Action<Vector2Int> Clicked;

        private readonly List<CellarMapRow> _rows = new();

        public ICell this[Vector2Int coordinate] => _rows[coordinate.y].Cells[coordinate.x];
        public ICell this[int x, int y] => _rows[y].Cells[x];

        public void Recreate(int width, int height)
        {
            foreach (CellarMapRow row in _rows)
            {
                Remove(row);
                for (int x = 0; x < row.Cells.Count; x += 1)
                {
                    row.Cells[x].Clicked -= InvokeClickedEvent;
                }
            }
            _rows.Clear();
            for (int rowIndex = 0; rowIndex < height; rowIndex += 1)
            {
                var row = new CellarMapRow(rowIndex, width);
                _rows.Add(row);
                Add(row);
                for (int x = 0; x < row.Cells.Count; x += 1)
                {
                    row.Cells[x].Clicked += InvokeClickedEvent;
                }
            }
        }

        public void UpdateCellViewData(int x, int y, CellViewData viewData)
        {
            _rows[x].Cells[y].UpdateView(viewData);
        }

        public void UpdateCellViewData(Vector2Int coordinate, CellViewData viewData)
        {
            UpdateCellViewData(coordinate.x, coordinate.y, viewData);
        }

        private void InvokeClickedEvent(Vector2Int coordinate)
        {
            Clicked?.Invoke(coordinate);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellarMap, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
