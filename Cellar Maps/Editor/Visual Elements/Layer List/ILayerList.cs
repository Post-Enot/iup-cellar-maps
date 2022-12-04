using System;
using System.Collections.Generic;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления списка слоёв клеточной карты.
    /// </summary>
    public interface ILayerList
    {
        public event Action<int> Added;
        public event Action<int> Removed;
        public event Action<int, int> MovedFromTo;
        public event Action<int, string, Color> ViewDataChanged;
        public event Action<int> Selected;
        public event Action Unselected;

        public void BindWith(IReadOnlyList<ILayerViewData> viewDataList);
    }
}
