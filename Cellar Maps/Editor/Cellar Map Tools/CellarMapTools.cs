using IUP.Toolkits.CellarMaps.Editor.UI;
using IUP.Toolkits.Matrices;

namespace IUP.Toolkits.CellarMaps.Editor
{
    public sealed class CellarMapTools
    {
        public CellarMapTools(
            IRecreateCommand recreateCommand,
            IResizeCommand resizeCommand,
            IRotateCommand rotateCommand,
            IMirrorCommand mirrorCommand,
            CellarMapPresenter cellarMapPresenter,
            PalettePresenter palettePresenter,
            LayerListPresenter layerListPresenter,
            ICellarMapInteractor cellarMapInteractor)
        {
            _recreateCommand = recreateCommand;
            _recreateCommand.RecreateCommandInvoked += RecreateMap;
            _resizeCommand = resizeCommand;
            _resizeCommand.ResizeCommandInvoked += ResizeMap;
            _rotateCommand = rotateCommand;
            _rotateCommand.RotateCommandInvoked += RotateMap;
            _mirrorCommand = mirrorCommand;
            _mirrorCommand.MirrorCommandInvoked += MirrorMap;
            _cellarMapPresenter = cellarMapPresenter;
            _cellarMapInteractor = cellarMapInteractor;
            _palettePresenter = palettePresenter;
            _layerListPresenter = layerListPresenter;
            _swipePoint = new SwipePoint(
                _cellarMapInteractor,
                _palettePresenter,
                _layerListPresenter,
                _cellarMapPresenter);
            _swipePoint.Enable();
        }

        private readonly IRecreateCommand _recreateCommand;
        private readonly IResizeCommand _resizeCommand;
        private readonly IRotateCommand _rotateCommand;
        private readonly IMirrorCommand _mirrorCommand;
        private readonly CellarMapPresenter _cellarMapPresenter;
        private readonly ICellarMapInteractor _cellarMapInteractor;
        private readonly PalettePresenter _palettePresenter;
        private readonly LayerListPresenter _layerListPresenter;
        private readonly SwipePoint _swipePoint;

        private void RecreateMap(int width, int height)
        {
            _cellarMapInteractor.Recreate(width, height);
            _cellarMapPresenter.UpdateCellarMapView();
        }

        private void ResizeMap(
            int width,
            int height,
            WidthResizeRule widthResizeRule,
            HeightResizeRule heightResizeRule)
        {
            _cellarMapInteractor.Resize(width, height, widthResizeRule, heightResizeRule);
            _cellarMapPresenter.UpdateCellarMapView();
        }

        private void RotateMap(MatrixRotation matrixRotation)
        {
            _cellarMapInteractor.Rotate(matrixRotation);
            _cellarMapPresenter.UpdateCellarMapView();
        }

        private void MirrorMap(MatrixMirror matrixMirror)
        {
            _cellarMapInteractor.Mirror(matrixMirror);
            _cellarMapPresenter.UpdateCellarMapView();
        }
    }
}
