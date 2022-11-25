using UnityEditor.UIElements;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.UI
{
    public class LayersListElement : ListViewElement<LayerViewData>
    {
        public LayersListElement() : base()
        {
            ColorField = new ColorField()
            {
                tooltip = "Цвет слоя. Служит исключительно для идентификации слоя в инспекторе."
            };
            LayerNameField = new TextField
            {
                label = "Layer name",
                value = "noname",
                tooltip = "Название слоя. Служит исключительно для идентификации слоя в инспекторе."
            };
            Add(ColorField);
            Add(LayerNameField);
            AddToClassList("cm-layer-element");
            LayerNameField.AddToClassList("cm-layer-element__layer-name-field");
            ColorField.AddToClassList("cm-layer-element__color-field");
        }

        public ColorField ColorField { get; }
        public TextField LayerNameField { get; }

        private LayerViewData _viewData;

        public override void BindWith(LayerViewData viewData)
        {
            if (_viewData == null)
            {
                LayerNameField.RegisterValueChangedCallback(UpdateLayerName);
                ColorField.RegisterValueChangedCallback(UpdateColorViewData);
            }
            _viewData = viewData;
            LayerNameField.value = viewData.LayerName;
            ColorField.value = viewData.Color;
        }

        private void UpdateLayerName(ChangeEvent<string> context)
        {
            _viewData.LayerName = context.newValue;
        }

        private void UpdateColorViewData(ChangeEvent<Color> context)
        {
            _viewData.Color = context.newValue;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<LayersListElement, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
