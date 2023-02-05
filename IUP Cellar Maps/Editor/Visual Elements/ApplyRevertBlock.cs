using System;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Блок кнопок для сохранения или отмены изменений.
    /// </summary>
    public sealed class ApplyRevertBlock : VisualElement
    {
        public ApplyRevertBlock()
        {
            AddToClassList("cm-apply-revert-block");
            _applyButton = new()
            {
                text = "Apply"
            };
            _applyButton.AddToClassList("cm-apply-revert-block__apply-button");
            _applyButton.clicked += () => Applied?.Invoke();
            Add(_applyButton);
            _revertButton = new()
            {
                text = "Revert"
            };
            _revertButton.AddToClassList("cm-apply-revert-block__revert-button");
            _revertButton.clicked += () => Revert?.Invoke();
            Add(_revertButton);
        }

        /// <summary>
        /// Вызывается при нажатии на кнопку "применить".
        /// </summary>
        public event Action Applied;
        /// <summary>
        /// Вызывается при нажатии на кнопку "вернуть назад".
        /// </summary>
        public event Action Revert;

        private readonly Button _applyButton;
        private readonly Button _revertButton;

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<ApplyRevertBlock, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
