using System;
using UnityEditor;
using UnityEngine;

namespace CellarMaps
{
    [CreateAssetMenu(fileName = "CellarMapObject", menuName = "Cellar CellarMapObject")]
    [Serializable]
    public class CellarMapAsset : ScriptableObject
    {
        public CellarMap Map => _cellarMap;
        public Palette Palette => Map.Palette;
        public ViewPalette ViewPalette => _viewPalette;

        [SerializeReference] private CellarMap _cellarMap;
        [SerializeReference] private ViewPalette _viewPalette;

        private void OnEnable()
        {
            if (Map == null)
            {
                Palette palette = new();
                _ = palette.CreateNewCellType();
                _viewPalette = new ViewPalette(palette);
                _cellarMap = new CellarMap(10, 10, palette);
            }
        }
    }
}
