using System.Collections.Generic;
using UnityEngine.UIElements;

namespace IUP_Toolkits.CellarMaps.UI
{
    public abstract class ListViewShell<T1, T2> : ListView where T1 : ListViewElement<T2>, new() where T2: class
    {
        public ListViewShell()
        {
            AddToClassList("cm-list-view");
            InitMakeItemCallback();
            InitBindItemCallback();
        }

        public void BindWith(List<T2> viewDataList)
        {
            itemsSource = viewDataList.ToArray();
        }

        private void InitMakeItemCallback()
        {
            static VisualElement MakeItem() => new T1();
            makeItem = MakeItem;
        }

        private void InitBindItemCallback()
        {
            void BindItem(VisualElement item, int i)
            {
                var paletteElement = item as T1;
                paletteElement.BindWith(itemsSource[i] as T2);
            }
            bindItem = BindItem;
        }
    }
}
