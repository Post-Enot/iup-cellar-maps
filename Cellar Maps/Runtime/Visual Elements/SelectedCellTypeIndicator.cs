using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP_Toolkits.CellarMaps.UI
{
    public sealed class SelectedCellTypeIndicator : VisualElement
    {
        public SelectedCellTypeIndicator()
        {
            Label = new Label()
            {
                text = "Selected Cell Type: "
            };
            IndicatorLabel = new Label()
            {
                text = _nullCellTypeViewDataStringValue,
                name = "cm-cell-type-indicator"
            };
            ResetButton = new Button()
            {
                text = "Reset"
            };
            ResetButton.clicked += InvokeResetButtonClickedEvent;
            Add(Label);
            Add(IndicatorLabel);
            Add(ResetButton);
            AddToClassList("cm-selected-cell-type-indicator");
            Label.AddToClassList("cm-selected-cell-type-indicator__label");
            IndicatorLabel.AddToClassList("cm-selected-cell-type-indicator__indicator");
            ResetButton.AddToClassList("cm-selected-cell-type-indicator__reset-button");
            _defaultBackgroundColor = style.backgroundColor;
        }
        
        ~SelectedCellTypeIndicator()
        {
            ResetButton.clicked -= InvokeResetButtonClickedEvent;
        }

        public readonly Label Label;
        public readonly Label IndicatorLabel;
        public readonly Button ResetButton;
        public CellTypeViewData IndicatedCellTypeViewData
        {
            get => _indicatedCellTypeViewData;
            set
            {
                _indicatedCellTypeViewData = value;
                UpdateView();
            }
        }
        public string NullCellTypeViewDataStringValue
        {
            get => _nullCellTypeViewDataStringValue;
            set
            {
                _nullCellTypeViewDataStringValue = value;
                if (_indicatedCellTypeViewData == null)
                {
                    UpdateView();
                }
            }
        }

        public event Action ResetButtonClicked;

        private readonly StyleColor _defaultBackgroundColor;
        private CellTypeViewData _indicatedCellTypeViewData;
        private string _nullCellTypeViewDataStringValue;

        private void UpdateView()
        {
            if (_indicatedCellTypeViewData != null)
            {
                IndicatorLabel.text = _indicatedCellTypeViewData.TypeName;
                IndicatorLabel.style.backgroundColor = _indicatedCellTypeViewData.Color;
            }
            else
            {
                IndicatorLabel.text = _nullCellTypeViewDataStringValue;
                IndicatorLabel.style.backgroundColor = _defaultBackgroundColor;
            }
        }

        private void InvokeResetButtonClickedEvent()
        {
            ResetButtonClicked?.Invoke();
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<SelectedCellTypeIndicator, UxmlTraits> { }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _nullCellTypeViewDataStringValueAttribute =
                new() { name = "Null String Value", defaultValue = "None" };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                var selectedCellTypeIndicator = visualElement as SelectedCellTypeIndicator;
                selectedCellTypeIndicator.NullCellTypeViewDataStringValue =
                    _nullCellTypeViewDataStringValueAttribute.GetValueFromBag(bag, context);
            }
        }
        #endregion
    }
}
