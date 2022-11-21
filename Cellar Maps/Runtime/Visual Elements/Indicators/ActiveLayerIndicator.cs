using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public sealed class ActiveLayerIndicator : VisualElement
    {
        public ActiveLayerIndicator()
        {
            IndicatorBlock = new VisualElement()
            {
                tooltip = "Активный слой, на котором размещаются клетки и который служит верхней границей для " +
                "отображения клеточного поля (все слои выше активного не отображаются)."
            };
            Label = new Label()
            {
                text = "Active layer: "
            };
            IndicatorLabel = new Label()
            {
                name = "cm-active-layer-indicator"
            };
            Add(IndicatorBlock);
            IndicatorBlock.Add(Label);
            IndicatorBlock.Add(IndicatorLabel);
            AddToClassList("cm-active-layer-indicator");
            Label.AddToClassList("cm-active-layer-indicator__label");
            IndicatorLabel.AddToClassList("cm-active-layer-indicator__indicator");
            IndicatorBlock.AddToClassList("cm-selected-cell-type-indicator__indicator-block");
        }

        ~ActiveLayerIndicator()
        {
            if (_activeLayerViewData != null)
            {
                _activeLayerViewData.ViewDataUpdated -= UpdateView;
            }
        }

        public Label Label { get; }
        public Label IndicatorLabel { get; }
        public VisualElement IndicatorBlock { get; }
        public LayerViewData ActiveLayerViewData
        {
            get => _activeLayerViewData;
            set
            {
                if (_activeLayerViewData != null)
                {
                    _activeLayerViewData.ViewDataUpdated -= UpdateView;
                }
                _activeLayerViewData = value;
                if (_activeLayerViewData != null)
                {
                    _activeLayerViewData.ViewDataUpdated += UpdateView;
                }
                UpdateView();
            }
        }

        private LayerViewData _activeLayerViewData;

        private void UpdateView()
        {
            if (_activeLayerViewData != null)
            {
                IndicatorLabel.text = _activeLayerViewData.LayerName;
            }
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<ActiveLayerIndicator, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
