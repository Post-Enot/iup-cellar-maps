using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace CellarMaps.UI
{
    public sealed class FieldCell : Button
    {
        public FieldCell()
        {
            _defaultBackgroundColor = style.backgroundColor;
        }

        public FieldCell(int x, int y, Action<Vector2Int> interactionCallback)
        {
            _coordinate = new Vector2Int(x, y);
            AddToClassList("cm-field-cell");
            UpdateTooltip();
            clicked += InvokeInteractionCallback;
            _defaultBackgroundColor = style.backgroundColor;
            _interactionCallback = interactionCallback;
        }

        ~FieldCell()
        {
            if (_viewData != null)
            {
                _viewData.ViewDataUpdated -= InitViewData;
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
        public new sealed class UxmlFactory : UxmlFactory<FieldCell, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
