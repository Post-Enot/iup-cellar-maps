using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    [Serializable]
    public sealed class LayerViewData
    {
        public LayerViewData(ICellarMapLayer layer, int index)
        {
            _layer = layer;
            _layerIndex = index;
            if (_layerIndex == 0)
            {
                _layerName = $"default-layer-{_layerIndex}";
            }
            else
            {
                _layerName = $"other-layer-{_layerIndex}";
            }
        }

        public ICellarMapLayer Layer => _layer;
        public string LayerName
        {
            get => _layerName;
            set
            {
                _layerName = value;
                ViewDataUpdated?.Invoke();
            }
        }
        public int LayerIndex
        {
            get => _layerIndex;
            set
            {
                _layerIndex = value;
                ViewDataUpdated?.Invoke();
            }
        }

        public event Action ViewDataUpdated;

        [SerializeField] private int _layerIndex;
        [SerializeField] private string _layerName;
        [SerializeReference] private ICellarMapLayer _layer;
    }
}
