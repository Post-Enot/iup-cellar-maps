using System;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps
{
    [CreateAssetMenu(fileName = "CellarMapAsset", menuName = "CellarMapAsset")]
    [Serializable]
    public class CellarMapAsset : ScriptableObject
    {
        public CellarMap Map => _cellarMap;
        public Palette Palette => Map.Palette;
        public ViewPalette ViewPalette => _viewPalette;
        public ViewLayers ViewLayers => _viewLayers;

        [SerializeReference] private CellarMap _cellarMap;
        [SerializeReference] private ViewPalette _viewPalette;
        [SerializeReference] private ViewLayers _viewLayers;

        private void OnEnable()
        {
            if (Map == null)
            {
                Palette palette = new();
                _ = palette.CreateNewCellType();
                _viewPalette = new ViewPalette(palette);
                _cellarMap = new CellarMap(10, 10, palette);
                _cellarMap.Layers.AddLayer();
                _viewLayers = new ViewLayers(_cellarMap.Layers);
            }
        }
    }
}
