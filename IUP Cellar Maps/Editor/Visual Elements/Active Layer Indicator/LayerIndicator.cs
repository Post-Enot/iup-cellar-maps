using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Визуальный элемент индикатора слоя клеточной карты.
    /// </summary>
    public sealed class LayerIndicator : Label, ILayerIndicator
    {
        public LayerIndicator()
        {
            AddToClassList("cm-layer-indicator");
        }

        public void SetViewData(ILayerViewData viewData)
        {
            text = viewData.Name;
            style.backgroundColor = viewData.Color;
            style.color = style.backgroundColor.value.MakeContrast(resolvedStyle.color);
        }

        #region UXML
        [Preserve]
        public new sealed class UxmlFactory : UxmlFactory<LayerIndicator, UxmlTraits> { }

        [Preserve]
        public new sealed class UxmlTraits : VisualElement.UxmlTraits { }
        #endregion
    }
}
