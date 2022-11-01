using UnityEngine;

namespace CellarMaps
{
    [CreateAssetMenu(fileName = "CellarMap", menuName = "Cellar CellarMap")]
    public class CellarMapAsset : ScriptableObject
    {
        public CellarMap Map { get; private set; }
        public Palette Palette => Map.Palette;
        public ViewPalette ViewPalette { get; private set; }

        private void OnEnable()
        {
            if (Map == null)
            {
                Palette palette = new();
                _ = palette.CreateNewCellType();
                ViewPalette = new ViewPalette(palette);
                Map = new CellarMap(10, 10, palette);
            }
        }
    }
}
