using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент индикатора активного слоя клеточной карты.
    /// </summary>
    public sealed class ActiveLayerIndicator : VisualElement, IActiveLayerIndicator
    {
        public ActiveLayerIndicator()
        {
            AddToClassList("cm-active-layer-indicator");
            _label = new Label()
            {
                text = "Active Layer"
            };
            _label.AddToClassList("cm-active-layer-indicator__label");
            _layerIndicator = new LayerIndicator();
            _layerIndicator.AddToClassList("cm-active-layer-indicator__indicator");
            Add(_label);
            Add(_layerIndicator);
        }

        private readonly Label _label;
        private readonly LayerIndicator _layerIndicator;

        public void SetViewData(ILayerViewData viewData)
        {
            _layerIndicator.SetViewData(viewData);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<ActiveLayerIndicator, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
