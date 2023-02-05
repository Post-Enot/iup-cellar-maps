using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Editor
{
    public abstract class CellarMapTool
    {
        public CellarMapTool(
            ICellarMapInteractor model,
            PalettePresenter palette,
            LayerListPresenter layerList,
            CellarMapPresenter presenter)
        {
            Model = model;
            Palette = palette;
            LayerList = layerList;
            Presenter = presenter;
        }

        public ICellarMapInteractor Model { get; }
        public PalettePresenter Palette { get; }
        public LayerListPresenter LayerList { get; }
        public CellarMapPresenter Presenter { get; }

        public abstract void Enable();

        public abstract void Disable();
    }
}
