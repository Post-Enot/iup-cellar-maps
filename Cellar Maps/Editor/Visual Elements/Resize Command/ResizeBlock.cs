using IUP.Toolkits.Matrices;
using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using System;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public sealed class ResizeBlock : VisualElement, IResizeCommand
    {
        public ResizeBlock()
        {
            AddToClassList("cm-resize-block");
            _widthOffsetField = new IntegerField()
            {
                label = "Width Offset",
                value = 0
            };
            _widthOffsetField.AddToClassList("cm-resize-block__width-offset-field");
            _widthOffsetField.RegisterValueChangedCallback((_) => UpdateButtonState());
            _heightOffsetField = new IntegerField()
            {
                label = "Height Offset",
                value = 0
            };
            _heightOffsetField.RegisterValueChangedCallback((_) => UpdateButtonState());
            _heightOffsetField.AddToClassList("cm-resize-block__height-offset-field");
            _resizeButton = new Button()
            {
                text = "Resize Map"
            };
            _widthResizeRuleField = new EnumField(WidthResizeRule.Right);
            _widthResizeRuleField.AddToClassList("cm-resize-block__width-resize-rule-field");
            _heightResizeRuleField = new EnumField(HeightResizeRule.Down);
            _heightResizeRuleField.AddToClassList("cm-resize-block__height-resize-rule-field");
            _resizeButton.clicked += () => ResizeCommandInvoked?.Invoke(
                _widthOffsetField.value,
                _heightOffsetField.value,
                (WidthResizeRule)_widthResizeRuleField.value,
                (HeightResizeRule)_heightResizeRuleField.value);
            _widthBlock = new VisualElement();
            _widthBlock.AddToClassList("cm-resize-block__width-block");
            _heightBlock = new VisualElement();
            _heightBlock.AddToClassList("cm-resize-block__height-block");
            Add(_widthBlock);
            Add(_heightBlock);
            _widthBlock.Add(_widthOffsetField);
            _widthBlock.Add(_widthResizeRuleField);
            _heightBlock.Add(_heightOffsetField);
            _heightBlock.Add(_heightResizeRuleField);
            Add(_resizeButton);
            UpdateButtonState();
        }

        public event Action<int, int, WidthResizeRule, HeightResizeRule> ResizeCommandInvoked;

        private readonly VisualElement _widthBlock;
        private readonly VisualElement _heightBlock;
        private readonly IntegerField _widthOffsetField;
        private readonly IntegerField _heightOffsetField;
        private readonly EnumField _widthResizeRuleField;
        private readonly EnumField _heightResizeRuleField;
        private readonly Button _resizeButton;

        private void UpdateButtonState()
        {
            if (_widthOffsetField.value == 0 && _heightOffsetField.value == 0)
            {
                _resizeButton.SetEnabled(false);
            }
            else
            {
                _resizeButton.SetEnabled(true);
            }
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<ResizeBlock, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
