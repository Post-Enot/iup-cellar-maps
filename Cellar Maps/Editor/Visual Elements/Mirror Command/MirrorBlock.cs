using IUP.Toolkits.Matrices;
using System;
using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    public class MirrorBlock : VisualElement, IMirrorCommand
    {
        public MirrorBlock()
        {
            AddToClassList("cm-mirror-block");
            _mirrorField = new EnumField(MatrixMirror.Horizontal)
            {
                label = "Mirror"
            };
            _mirrorButton = new Button()
            {
                text = "Mirror Map"
            };
            _mirrorButton.clicked += () => MirrorCommandInvoked?.Invoke(
                (MatrixMirror)_mirrorField.value);
            Add(_mirrorField);
            Add(_mirrorButton);
        }

        public event Action<MatrixMirror> MirrorCommandInvoked;

        private readonly EnumField _mirrorField;
        private readonly Button _mirrorButton;

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<MirrorBlock, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
