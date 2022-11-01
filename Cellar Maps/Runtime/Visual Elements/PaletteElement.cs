using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace CellarMaps.UI
{
    public sealed class PaletteElement : VisualElement
    {
        public PaletteElement()
        {
            ColorField = new ColorField();
            ColorField.AddToClassList("cm-color-field-palette-element");
            TypeNameField = new TextField
            {
                label = "Type name",
                value = "noname",
                tooltip = "Название типа клетки, с помощью которого классы-генераторы идентифицируют и " +
                "сопоставляют её тип с необходимой заменой.\n" +
                "Разные клетки с одинаковым названием типа сопоставляются с одной и той же заменой."
            };
            TypeNameField.AddToClassList("cm-name-field-palette-element");
            AddToClassList("cm-palette-element");
            Add(TypeNameField);
            Add(ColorField);
        }

        public readonly ColorField ColorField;
        public readonly TextField TypeNameField;

        private CellTypeViewData _viewData;

        public void BindWith(CellTypeViewData viewData)
        {
            if (_viewData == null)
            {
                TypeNameField.RegisterValueChangedCallback(UpdateTypeNameViewData);
                ColorField.RegisterValueChangedCallback(UpdateColorViewData);
            }
            _viewData = viewData;
            TypeNameField.value = viewData.TypeName;
            ColorField.value = viewData.Color;
        }

        private void UpdateTypeNameViewData(ChangeEvent<string> context)
        {
            _viewData.TypeName = context.newValue;
        }

        private void UpdateColorViewData(ChangeEvent<Color> context)
        {
            _viewData.Color = context.newValue;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<PaletteElement, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
