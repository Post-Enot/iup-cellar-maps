using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public sealed class PaletteElement : VisualElement, IPaletteElement
    {
        public PaletteElement() : base()
        {
            AddToClassList("cm-palette-element");
            _applyRevertBlock = new ApplyRevertBlock();
            _applyRevertBlock.Applied += InvokeViewDataChangedEvent;
            _applyRevertBlock.Revert += RevertViewData;
            _cellTypeNameField = new TextField();
            _ = _cellTypeNameField.RegisterValueChangedCallback((_) => UpdateApplyRevertBlock());
            _cellTypeColorField = new ColorField();
            _ = _cellTypeColorField.RegisterValueChangedCallback((_) => UpdateApplyRevertBlock());
            Add(_cellTypeNameField);
            Add(_cellTypeColorField);
            Add(_applyRevertBlock);
        }

        public event Action<string, string, Color> ViewDataChanged;

        private readonly TextField _cellTypeNameField;
        private readonly ColorField _cellTypeColorField;
        private readonly ApplyRevertBlock _applyRevertBlock;
        private ICellTypeViewData _bindedViewData;
        private bool _isApplyRevertButtonShown = true;

        public void BindWith(ICellTypeViewData viewData)
        {
            _bindedViewData = viewData;
            _cellTypeColorField.SetValueWithoutNotify(_bindedViewData.Color);
            _cellTypeNameField.SetValueWithoutNotify(_bindedViewData.CellTypeName);
            UpdateApplyRevertBlock();
        }

        private void UpdateApplyRevertBlock()
        {
            if ((_bindedViewData.CellTypeName == _cellTypeNameField.value) &&
                (_bindedViewData.Color == _cellTypeColorField.value))
            {
                if (_isApplyRevertButtonShown)
                {
                    Remove(_applyRevertBlock);
                    _isApplyRevertButtonShown = false;
                }
            }
            else
            {
                if (!_isApplyRevertButtonShown)
                {
                    Add(_applyRevertBlock);
                    _isApplyRevertButtonShown = true;
                }
            }
        }

        private void RevertViewData()
        {
            _cellTypeColorField.SetValueWithoutNotify(_bindedViewData.Color);
            _cellTypeNameField.SetValueWithoutNotify(_bindedViewData.CellTypeName);
        }

        private void InvokeViewDataChangedEvent()
        {
            ViewDataChanged?.Invoke(
                _bindedViewData.CellTypeName,
                _cellTypeNameField.value,
                _cellTypeColorField.value);
            UpdateApplyRevertBlock();
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<PaletteElement, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
