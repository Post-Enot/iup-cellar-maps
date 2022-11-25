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
            _color = new(30f / 255f, 30f / 255f, 30f / 255f, 1);
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
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                ViewDataUpdated?.Invoke();
            }
        }

        public event Action ViewDataUpdated;

        [SerializeField] private int _layerIndex;
        [SerializeField] private string _layerName;
        [SerializeField] private Color _color;
        [SerializeReference] private ICellarMapLayer _layer;
    }
}
