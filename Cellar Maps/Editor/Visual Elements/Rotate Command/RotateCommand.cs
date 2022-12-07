using IUP.Toolkits.Matrices;
using System;
using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public sealed class RotateBlock : VisualElement, IRotateCommand
    {
        public RotateBlock()
        {
            AddToClassList("cm-rotate-block");
            _rotationField = new EnumField(MatrixRotation.Clockwise90_Degrees)
            {
                label = "Rotation"
            };
            _rotateButton = new Button()
            {
                text = "Rotate Map"
            };
            _rotateButton.clicked += () => RotateCommandInvoked?.Invoke(
                (MatrixRotation)_rotationField.value);
            Add(_rotationField);
            Add(_rotateButton);
        }

        public event Action<MatrixRotation> RotateCommandInvoked;

        private readonly EnumField _rotationField;
        private readonly Button _rotateButton;

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<RotateBlock, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
