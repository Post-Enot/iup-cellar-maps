using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public class LayersListElement : ListViewElement<LayerViewData>
    {
        public LayersListElement() : base()
        {
            LayerNameField = new TextField
            {
                label = "Layer name",
                value = "noname",
                tooltip = "Название слоя. Служит исключительно для идентификации слоя в инспекторе."
            };
            Add(LayerNameField);
            AddToClassList("cm-layer-element");
            LayerNameField.AddToClassList("cm-layer-element__layer-name-field");
        }

        public TextField LayerNameField { get; }

        private LayerViewData _viewData;

        public override void BindWith(LayerViewData viewData)
        {
            if (_viewData == null)
            {
                LayerNameField.RegisterValueChangedCallback(UpdateLayerName);
            }
            _viewData = viewData;
            LayerNameField.value = viewData.LayerName;
        }

        private void UpdateLayerName(ChangeEvent<string> context)
        {
            _viewData.LayerName = context.newValue;
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<LayersListElement, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
