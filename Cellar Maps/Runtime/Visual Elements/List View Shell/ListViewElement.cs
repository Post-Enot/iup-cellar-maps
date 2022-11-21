using UnityEngine.UIElements;

namespace IUP.Toolkits.CellarMaps.UI
{
    public abstract class ListViewElement<T> : VisualElement where T : class
    {
        public ListViewElement()
        {
            AddToClassList("cm-list-view-element");
        }

        public abstract void BindWith(T viewData);
    }
}
