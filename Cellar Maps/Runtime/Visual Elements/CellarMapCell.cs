using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP_Toolkits.CellarMaps.UI
{
    public sealed class CellarMapCell : Button
    {
        public CellarMapCell()
        {
            _defaultBackgroundColor = style.backgroundColor;
        }

        public CellarMapCell(int x, int y, Action<Vector2Int> interactionCallback)
        {
            _coordinate = new Vector2Int(x, y);
            AddToClassList("cm-cellar-map-cell");
            UpdateTooltip();
            clicked += InvokeInteractionCallback;
            _defaultBackgroundColor = style.backgroundColor;
            _interactionCallback = interactionCallback;
        }

        ~CellarMapCell()
        {
            if (_viewData != null)
            {
                _viewData.ViewDataUpdated -= InitViewData;
                clicked -= InvokeInteractionCallback;
            }
        }

        private void InvokeInteractionCallback()
        {
            _interactionCallback(Coordinate);
        }

        public Vector2Int Coordinate => _coordinate;

        private readonly Vector2Int _coordinate;
        private readonly StyleColor _defaultBackgroundColor;
        private readonly Action<Vector2Int> _interactionCallback;

        public CellTypeViewData ViewData
        {
            get => _viewData;
            set
            {
                if (_viewData != null)
                {
                    _viewData.ViewDataUpdated -= InitViewData;
                }
                _viewData = value;
                if (_viewData != null)
                {
                    _viewData.ViewDataUpdated += InitViewData;
                }
                InitViewData();
            }
        }

        private CellTypeViewData _viewData;

        private void InitViewData()
        {
            if (_viewData != null)
            {
                style.backgroundColor = new StyleColor(_viewData.Color);
            }
            else
            {
                style.backgroundColor = _defaultBackgroundColor;
            }
            UpdateTooltip();
        }

        private void UpdateTooltip()
        {
            tooltip = $"{_coordinate}\n";
            if (_viewData == null)
            {
                tooltip += "null";
            }
            else
            {
                tooltip += $"{_viewData.TypeName}";
            }
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<CellarMapCell, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
