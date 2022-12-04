using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальные данные клетки.
    /// </summary>
    public sealed record CellViewData
    {
        /// <summary>
        /// Цвет клетки.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Надпись, отображаемая на клетке.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Всплывающая подсказка, отображающаяся при задержке курсора над клеткой.
        /// </summary>
        public string Tooltip { get; set; }
    }
}
