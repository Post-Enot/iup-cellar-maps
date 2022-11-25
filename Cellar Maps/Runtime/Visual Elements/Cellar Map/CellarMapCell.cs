using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class CellarMapCell : Button
    {
        public CellarMapCell()
        {
            _defaultBackgroundColor = style.backgroundColor;
        }

        public CellarMapCell(int x, int y, Action<Vector2Int> interactionCallback)
        {
            Coordinate = new Vector2Int(x, y);
            AddToClassList("cm-cellar-map-cell");
            UpdateTooltip();
            clicked += InvokeInteractionCallback;
            _defaultBackgroundColor = style.backgroundColor;
            _interactionCallback = interactionCallback;
        }

        ~CellarMapCell()
        {
            if (ViewData != null)
            {
                ViewData.ViewDataUpdated -= InitViewData;
                clicked -= InvokeInteractionCallback;
            }
        }

        public Vector2Int Coordinate { get; }
        public CellTypeViewData ViewData { get; private set; }
        public bool IsOnActiveLayer { get; private set; }
        public CellType CellType => ViewData?.Type;

        private readonly StyleColor _defaultBackgroundColor;
        private readonly Action<Vector2Int> _interactionCallback;

        public void SetViewData(CellTypeViewData viewData, bool isOnActiveLayer)
        {
            if (ViewData != null)
            {
                ViewData.ViewDataUpdated -= InitViewData;
            }
            ViewData = viewData;
            IsOnActiveLayer = isOnActiveLayer;
            if (ViewData != null)
            {
                ViewData.ViewDataUpdated += InitViewData;
            }
            InitViewData();
        }

        private void InitViewData()
        {
            if (ViewData != null)
            {
                if (IsOnActiveLayer)
                {
                    style.backgroundColor = new StyleColor(ViewData.Color);
                }
                else
                {
                    style.backgroundColor = CalculateBackgroundColor(ViewData.Color);
                }
            }
            else
            {
                style.backgroundColor = _defaultBackgroundColor;
            }
            UpdateTooltip();
        }

        private StyleColor CalculateBackgroundColor(Color color)
        {
            Color.RGBToHSV(color, out float H, out float S, out float V);
            if (V >= 0.5f)
            {
                V -= 0.05f;
            }
            else
            {
                V += 0.05f;
            }
            return new StyleColor(Color.HSVToRGB(H, S, V));
        }

        private void UpdateTooltip()
        {
            tooltip = $"{Coordinate}\n";
            if (ViewData == null)
            {
                tooltip += "null";
            }
            else
            {
                tooltip += $"{ViewData.TypeName}";
            }
        }

        private void InvokeInteractionCallback()
        {
            _interactionCallback(Coordinate);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellarMapCell, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
