using System;
using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный компонент представления команды пересоздания клеточной карты.
    /// </summary>
    public sealed class RecreateBlock : VisualElement, IRecreateCommand
    {
        public RecreateBlock()
        {
            AddToClassList("cm-recreate-block");
            _widthField = new IntegerField()
            {
                label = "Width",
                value = 0
            };
            _widthField.AddToClassList("cm-recreate-block__width-field");
            _widthField.RegisterValueChangedCallback((_) => UpdateButtonState());
            _heightField = new IntegerField()
            {
                label = "Height",
                value = 0
            };
            _heightField.RegisterValueChangedCallback((_) => UpdateButtonState());
            _heightField.AddToClassList("cm-recreate-block__height-field");
            _recreateButton = new Button()
            {
                text = "Recreate Map"
            };
            _recreateButton.clicked += () => RecreateCommandInvoked?.Invoke(
                _widthField.value,
                _heightField.value);
            Add(_widthField);
            Add(_heightField);
            Add(_recreateButton);
            UpdateButtonState();
        }

        public event Action<int, int> RecreateCommandInvoked;

        private readonly IntegerField _widthField;
        private readonly IntegerField _heightField;
        private readonly Button _recreateButton;

        private void UpdateButtonState()
        {
            if (_widthField.value < 1 || _heightField.value < 1)
            {
                _recreateButton.SetEnabled(false);
            }
            else
            {
                _recreateButton.SetEnabled(true);
            }
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<RecreateBlock, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
