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
            IndicatorBlock = new VisualElement()
            {
                tooltip = "Выбранный тип клетки, используемый в процессе рисования паттернов клеточных карт."
            };
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
                text = "Reset",
                tooltip = "Сбрасывает значение выбранного типа клетки до значения пустой клетки."
            };
            ResetButton.clicked += InvokeResetButtonClickedEvent;
            Add(IndicatorBlock);
            IndicatorBlock.Add(Label);
            IndicatorBlock.Add(IndicatorLabel);
            Add(ResetButton);
            IndicatorBlock.AddToClassList("cm-selected-cell-type-indicator__indicator-block");
            AddToClassList("cm-selected-cell-type-indicator");
            Label.AddToClassList("cm-selected-cell-type-indicator__label");
            IndicatorLabel.AddToClassList("cm-selected-cell-type-indicator__indicator");
            ResetButton.AddToClassList("cm-selected-cell-type-indicator__reset-button");
            _defaultBackgroundColor = style.backgroundColor;
        }

        ~SelectedCellTypeIndicator()
        {
            ResetButton.clicked -= InvokeResetButtonClickedEvent;
            if (_selectedCellTypeViewData != null)
            {
                _selectedCellTypeViewData.ViewDataUpdated -= UpdateView;
            }
        }

        public VisualElement IndicatorBlock { get; }
        public Label Label { get; }
        public Label IndicatorLabel { get; }
        public Button ResetButton { get; }
        public CellTypeViewData SelectedCellTypeViewData
        {
            get => _selectedCellTypeViewData;
            set
            {
                if (_selectedCellTypeViewData != null)
                {
                    _selectedCellTypeViewData.ViewDataUpdated -= UpdateView;
                }
                _selectedCellTypeViewData = value;
                if (_selectedCellTypeViewData != null)
                {
                    _selectedCellTypeViewData.ViewDataUpdated += UpdateView;
                }
                UpdateView();
            }
        }
        public string NullCellTypeViewDataStringValue
        {
            get => _nullCellTypeViewDataStringValue;
            set
            {
                _nullCellTypeViewDataStringValue = value;
                if (_selectedCellTypeViewData == null)
                {
                    UpdateView();
                }
            }
        }

        public event Action ResetButtonClicked;

        private readonly StyleColor _defaultBackgroundColor;
        private CellTypeViewData _selectedCellTypeViewData;
        private string _nullCellTypeViewDataStringValue;

        private void UpdateView()
        {
            if (_selectedCellTypeViewData != null)
            {
                IndicatorLabel.text = _selectedCellTypeViewData.TypeName;
                IndicatorLabel.style.backgroundColor = _selectedCellTypeViewData.Color;
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
