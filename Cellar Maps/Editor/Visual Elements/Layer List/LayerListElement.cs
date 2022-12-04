using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public sealed class LayerListElement : VisualElement
    {
        public LayerListElement() : base()
        {
            AddToClassList("cm-layer-list-element");
            _applyRevertBlock = new ApplyRevertBlock();
            _applyRevertBlock.Applied += InvokeViewDataChangedEvent;
            _applyRevertBlock.Revert += RevertViewData;
            _layerNameField = new TextField();
            _ = _layerNameField.RegisterValueChangedCallback((_) => UpdateApplyRevertBlock());
            _layerColorField = new ColorField();
            _ = _layerColorField.RegisterValueChangedCallback((_) => UpdateApplyRevertBlock());
            Add(_layerNameField);
            Add(_layerColorField);
            Add(_applyRevertBlock);
        }

        public event Action<ILayerViewData, string, Color> ViewDataChanged;

        private readonly TextField _layerNameField;
        private readonly ColorField _layerColorField;
        private readonly ApplyRevertBlock _applyRevertBlock;
        private ILayerViewData _bindedViewData;
        private bool _isApplyRevertButtonShown = true;

        public void BindWith(ILayerViewData viewData)
        {
            _bindedViewData = viewData;
            _layerColorField.SetValueWithoutNotify(_bindedViewData.Color);
            _layerNameField.SetValueWithoutNotify(_bindedViewData.Name);
            UpdateApplyRevertBlock();
        }

        private void UpdateApplyRevertBlock()
        {
            if ((_bindedViewData.Name == _layerNameField.value) &&
                (_bindedViewData.Color == _layerColorField.value))
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
            _layerColorField.SetValueWithoutNotify(_bindedViewData.Color);
            _layerNameField.SetValueWithoutNotify(_bindedViewData.Name);
        }

        private void InvokeViewDataChangedEvent()
        {
            ViewDataChanged?.Invoke(
                _bindedViewData,
                _layerNameField.value,
                _layerColorField.value);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<PaletteElement, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
