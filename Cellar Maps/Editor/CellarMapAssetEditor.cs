using IUP.Toolkits.CellarMaps.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using IUP.Toolkits.Matrices;

namespace IUP.Toolkits.CellarMaps.EditorScripts
{
    [CustomEditor(typeof(CellarMapAsset))]
    public sealed class CellarMapAssetEditor : Editor
    {
        private CellarMapAsset _cellarMapAsset;
        private VisualElement _rootElement;
        private VisualTreeAsset _visualTree;

        private IntegerField _uiRecreateWidthField;
        private IntegerField _uiRecreateHeightField;
        private Button _uiRecreateMapButton;

        private IntegerField _uiResizeWidthOffsetField;
        private IntegerField _uiResizeHeightOffsetField;
        private EnumField _uiResizeWidthRuleField;
        private EnumField _uiResizeHeightRuleField;
        private Button _uiResizeMapButton;

        private UI.CellarMap _uiCellarMap;
        private PaletteList _uiPalette;
        private LayersList _uiLayers;
        private SelectedCellTypeIndicator _uiSelectedCellTypeIndicator;
        private ActiveLayerIndicator _uiActiveLayerIndicator;

        private ListShellPresenter<LayersListElement, LayerViewData> _layersPresenter;
        private ListShellPresenter<PaletteListElement, CellTypeViewData> _palettePresenter;
        private CellarMapPresenter _cellarMapPresenter;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = _rootElement;
            root.Clear();
            _visualTree.CloneTree(root);
            InitUI_References(root);
            if (_layersPresenter != null)
            {
                _layersPresenter.OnDisable();
                _palettePresenter.OnDisable();
                _cellarMapPresenter.OnDisable();
            }
            _layersPresenter = new(_cellarMapAsset.ViewLayers, _uiLayers);
            _palettePresenter = new(_cellarMapAsset.ViewPalette, _uiPalette);
            _cellarMapPresenter = new(
                _cellarMapAsset.Map,
                _uiCellarMap,
                _cellarMapAsset.ViewPalette,
                _cellarMapAsset.ViewLayers);
            _layersPresenter.OnEnable();
            _palettePresenter.OnEnable();
            _cellarMapPresenter.OnEnable();
            _uiRecreateMapButton.clicked += RecreateMap;
            _uiResizeMapButton.clicked += ResizeMap;
            InitSelectedCellTypeIndicator();
            InitActiveLayerIndicator();
            InitRecreateMapSizeFields();
            InitResizeMapBlockFields();
            EditorUtility.SetDirty(_cellarMapAsset);
            return root;
        }

        private void OnEnable()
        {
            _cellarMapAsset = target as CellarMapAsset;
            _rootElement = new VisualElement();
            _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Packages/com.iup.cellar-maps/Runtime/CellarMapsEditorWindow.uxml");
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Packages/com.iup.cellar-maps/Runtime/CellarMapAssetEditorStyleSheet.uss");
            _rootElement.styleSheets.Add(uss);
        }

        private void InitUI_References(VisualElement root)
        {
            _uiRecreateWidthField = root.Q<IntegerField>("recreate-width-int-field");
            _uiRecreateHeightField = root.Q<IntegerField>("recreate-height-int-field");
            _uiRecreateMapButton = root.Q<Button>("recreate-cellar-map-button");

            _uiResizeWidthOffsetField = root.Q<IntegerField>("resize-width-int-field");
            _uiResizeHeightOffsetField = root.Q<IntegerField>("resize-height-int-field");
            _uiResizeWidthRuleField = root.Q<EnumField>("resize-width-rule-enum-field");
            _uiResizeHeightRuleField = root.Q<EnumField>("resize-height-rule-enum-field");
            _uiResizeMapButton = root.Q<Button>("resize-cellar-map-button");

            _uiCellarMap = root.Q<UI.CellarMap>("cellar-map");
            _uiPalette = root.Q<UI.PaletteList>("palette");
            _uiSelectedCellTypeIndicator = root.Q<SelectedCellTypeIndicator>("selected-cell-type-indicator");
            _uiActiveLayerIndicator = root.Q<ActiveLayerIndicator>("active-layer-indicator");
            _uiLayers = root.Q<LayersList>("layers");
        }

        private void InitSelectedCellTypeIndicator()
        {
            _cellarMapAsset.ViewPalette.SelectedCellTypeViewDataChanged +=
                HandleViewPaletteSelectedCellTypeViewDataChanged;
            _uiSelectedCellTypeIndicator.ResetButtonClicked += HandleSelectedCellTypeIndicatorResetButtonClick;
            _uiSelectedCellTypeIndicator.SelectedCellTypeViewData =
                _cellarMapAsset.ViewPalette.SelectedCellTypeViewData;
        }

        private void InitActiveLayerIndicator()
        {
            _cellarMapAsset.ViewLayers.ActiveLayerChanged += HandleViewLayerActiveLayerViewDataChanged;
            _uiActiveLayerIndicator.ActiveLayerViewData = _cellarMapAsset.ViewLayers.ActiveLayerViewData;
        }

        private void HandleViewLayerActiveLayerViewDataChanged(LayerViewData viewData)
        {
            _uiActiveLayerIndicator.ActiveLayerViewData = viewData;
        }

        private void HandleSelectedCellTypeIndicatorResetButtonClick()
        {
            _cellarMapAsset.ViewPalette.SelectedCellTypeViewData = null;
        }

        private void HandleViewPaletteSelectedCellTypeViewDataChanged(CellTypeViewData viewData)
        {
            _uiSelectedCellTypeIndicator.SelectedCellTypeViewData = viewData;
        }

        private void InitRecreateMapSizeFields()
        {
            _uiRecreateWidthField.value = _cellarMapAsset.Map.Width;
            _uiRecreateHeightField.value = _cellarMapAsset.Map.Height;
        }

        private void InitResizeMapBlockFields()
        {
            _uiResizeWidthRuleField.Init(WidthResizeRule.Right);
            _uiResizeHeightRuleField.Init(HeightResizeRule.Down);
        }

        private void ResizeMap()
        {
            EditorUtility.SetDirty(_cellarMapAsset);
            _cellarMapAsset.Map.Resize(
                _uiResizeWidthOffsetField.value,
                _uiResizeHeightOffsetField.value,
                (WidthResizeRule)_uiResizeWidthRuleField.value,
                (HeightResizeRule)_uiResizeHeightRuleField.value);
        }

        private void RecreateMap()
        {
            EditorUtility.SetDirty(_cellarMapAsset);
            _cellarMapAsset.Map.Recreate(_uiRecreateWidthField.value, _uiRecreateHeightField.value);
        }
    }
}
