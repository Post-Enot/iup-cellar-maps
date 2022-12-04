using IUP.Toolkits.CellarMaps.Editor.UI;
using IUP.Toolkits.CellarMaps.Serialization;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps.Editor
{
    internal sealed class CellarMapEditorWindow : EditorWindow, ISerializationCallbackReceiver
    {
        [SerializeField] private CellarMapAsset _asset;
        [SerializeField] private DTO.CellarMapFile _cellarMapFileDTO;

        private ICellarMapInteractor _cellarMapInteractor;

        private CellarMapPresenter _cellarMapPresenter;
        private PalettePresenter _palettePresenter;
        private LayerListPresenter _layerListPresenter;
        private CellarMapTools _cellarMapTools;

        private UI.ICellarMap _uiCellarMap;
        private UI.IPalette _uiPalette;
        private UI.ILayerList _uiLayerList;
        private UI.IRecreateCommand _uiRecreateCommand;
        private UI.IResizeCommand _uiResizeCommand;
        private UI.IActiveCellTypeIndicator _uiActiveCellTypeIndicator;
        private UI.IActiveLayerIndicator _uiActiveLayerIndicator;
        private ShortcutManager _shortcutManager;

        private void Awake()
        {
            saveChangesMessage = "You have unsaved changes. Would you like to keep them?";
            titleContent.text = "Cellar Map Editor";
        }

        private void CreateGUI()
        {
            InitCellarMapInteractor();
            _cellarMapInteractor.SetMarkUnsavedChangesCallback(MarkUnsavedChanges);
            VisualElement root = rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                UI_FilePathes.UXML_CellarMapAssetInspector);
            visualTree.CloneTree(root);
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(UI_FilePathes.USS_StyleSheetPath);
            root.styleSheets.Add(uss);
            _uiPalette = rootVisualElement.Q<UI.Palette>("palette");
            _uiCellarMap = rootVisualElement.Q<UI.CellarMap>("cellar-map");
            _uiLayerList = rootVisualElement.Q<UI.LayerList>("layer-list");
            _uiRecreateCommand = rootVisualElement.Q<RecreateBlock>("recreate-block");
            _uiResizeCommand = rootVisualElement.Q<ResizeBlock>("resize-block");
            _uiActiveCellTypeIndicator = rootVisualElement.Q<ActiveCellTypeIndicator>(
                "active-cell-type-indicator");
            _uiActiveLayerIndicator = rootVisualElement.Q<ActiveLayerIndicator>("active-layer-indicator");
            _cellarMapPresenter = new CellarMapPresenter(
                _cellarMapInteractor,
                _uiCellarMap,
                0.08f,
                "None",
                UI_Colors.Gray,
                "*");
            _palettePresenter = new PalettePresenter(
                _cellarMapInteractor,
                _uiPalette,
                _uiActiveCellTypeIndicator,
                "None",
                default,
                _cellarMapPresenter,
                "type-name",
                UI_Colors.DarkGray);
            _layerListPresenter = new LayerListPresenter(
                _cellarMapInteractor,
                _uiLayerList,
                _uiActiveLayerIndicator,
                _cellarMapPresenter,
                "layer-name",
                UI_Colors.DarkGray,
                0);
            _cellarMapTools = new CellarMapTools(
                _uiRecreateCommand,
                _uiResizeCommand,
                _cellarMapPresenter,
                _palettePresenter,
                _layerListPresenter,
                _cellarMapInteractor);
            _shortcutManager = new ShortcutManager(rootVisualElement);
            InitShortcuts();
        }

        public override void SaveChanges()
        {
            (DTO.CellarMapViewData cellarMapViewDataDTO, DTO.CellarMap cellarMapDTO)
                = _cellarMapInteractor.ToDTO();
            var cellarMapFile = new DTO.CellarMapFile()
            {
                data_format_version = _cellarMapFileDTO.data_format_version,
                cellar_map_view_data = cellarMapViewDataDTO,
                cellar_map = cellarMapDTO
            };
            string cellarMapJson = CellarMapSerializer.CellarMapFileDTO_ToJson(cellarMapFile);
            File.WriteAllText(_asset.FilePath, cellarMapJson);
            hasUnsavedChanges = false;
            Repaint();
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            var path = AssetDatabase.GetAssetPath(instanceId);
            if (!path.EndsWith(CellarMapImporter._fileExtension,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            UnityEngine.Object assetObject = EditorUtility.InstanceIDToObject(instanceId);
            var asset = assetObject as CellarMapAsset;
            if (asset == null)
            {
                return false;
            }
            _ = OpenEditor(asset);
            return true;
        }

        public static CellarMapEditorWindow OpenEditor(CellarMapAsset asset)
        {
            CellarMapEditorWindow window = FindEditorForAsset(asset);
            if (window == null)
            {
                window = CreateInstance<CellarMapEditorWindow>();
                window.SetAsset(asset);
            }
            window.Show();
            window.Focus();
            return window;
        }

        public static CellarMapEditorWindow FindEditorForAsset(CellarMapAsset asset)
        {
            var windows = Resources.FindObjectsOfTypeAll<CellarMapEditorWindow>();
            return windows.FirstOrDefault(window => window._asset == asset);
        }

        private void SaveChangesShortcutCallback()
        {
            if (hasUnsavedChanges)
            {
                SaveChanges();
            }
        }

        private void InitShortcuts()
        {
            var saveShortcut = new Shortcut(SaveChangesShortcutCallback, KeyCode.LeftControl, KeyCode.S);
            _shortcutManager.RegisterShortcut(saveShortcut);
        }

        private void SetAsset(CellarMapAsset asset)
        {
            _asset = asset;
        }

        private void MarkUnsavedChanges()
        {
            hasUnsavedChanges = true;
        }

        private void InitCellarMapInteractor()
        {
            if (_cellarMapFileDTO == null)
            {
                string cellarMapJson = File.ReadAllText(_asset.FilePath);
                _cellarMapFileDTO = CellarMapSerializer.JsonToCellarMapFileDTO(cellarMapJson);
            }
            _cellarMapInteractor = CellarMapInteractor.DTO_ToCellarMapInteractor(
                _cellarMapFileDTO);
        }

        public void OnBeforeSerialize()
        {
            (DTO.CellarMapViewData cellarMapViewDataDTO, DTO.CellarMap cellarMapDTO)
                = _cellarMapInteractor.ToDTO();
            _cellarMapFileDTO.cellar_map_view_data = cellarMapViewDataDTO;
            _cellarMapFileDTO.cellar_map = cellarMapDTO;
        }

        public void OnAfterDeserialize() { }
    }
}
