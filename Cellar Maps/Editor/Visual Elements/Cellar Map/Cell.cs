using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент клетки клеточной карты.
    /// </summary>
    public sealed class Cell : Button, ICell
    {
        /// <summary>
        /// Инициализирует визуальный элемент клетки.
        /// </summary>
        public Cell()
        {
            AddToClassList("cm-cellar-map-cell");
            clicked += () => Clicked?.Invoke(Coordinate);
        }

        /// <summary>
        /// Инициализирует визуальный элемент клетки.
        /// </summary>
        /// <param name="coordinate">Координаты клетки на клеточной карте.</param>
        public Cell(Vector2Int coordinate) : this()
        {
            Coordinate = coordinate;
        }

        /// <summary>
        /// Инициализирует визуальный элемент клетки.
        /// </summary>
        /// <param name="x">X-компонента координаты клетки на клеточной карте.</param>
        /// <param name="y">Y-компонента координаты клетки на клеточной карте.</param>
        public Cell(int x, int y) : this(new Vector2Int(x, y)) { }

        public Vector2Int Coordinate { get; set; }

        public event Action<Vector2Int> Clicked;

        public void UpdateView(CellViewData viewData)
        {
            style.backgroundColor = viewData.Color;
            style.color = style.backgroundColor.value.MakeContrast(resolvedStyle.color);
            text = viewData.Title;
            tooltip = viewData.Tooltip;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<Cell, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
