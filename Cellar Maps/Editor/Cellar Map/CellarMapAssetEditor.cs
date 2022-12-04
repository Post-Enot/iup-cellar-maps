using IUP.Toolkits.CellarMaps.Serialization;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps.Editor
{
    [CustomEditor(typeof(CellarMapAsset))]
    public sealed class CellarMapAssetEditor : UnityEditor.Editor
    {
        private CellarMapAsset _asset;

        private VisualElement _rootElement;
        private VisualTreeAsset _visualTree;

        private UI.CellarMap _uiCellarMap;
        private CellarMapPresenter _cellarMapPresenter;
        private ICellarMapInteractor _cellarMapInteractor;

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = _rootElement;
            root.Clear();
            _visualTree.CloneTree(root);
            _uiCellarMap = _rootElement.Q<UI.CellarMap>("cellar-map");
            _cellarMapPresenter = new CellarMapPresenter(
                _cellarMapInteractor,
                _uiCellarMap,
                0.2f,
                "None",
                default,
                "*");
            EditorUtility.SetDirty(_asset);
            return root;
        }

        private void OnEnable()
        {
            _asset = target as CellarMapAsset;
            _rootElement = new VisualElement();
            _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                UI_FilePathes.UXML_CellarMapAssetInspector);
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(UI_FilePathes.USS_StyleSheetPath);
            _rootElement.styleSheets.Add(uss);
            LoadCellarMapInteractor();
        }

        private void LoadCellarMapInteractor()
        {
            string cellarMapJson = File.ReadAllText(_asset.FilePath);
            DTO.CellarMapFile cellarMapFileDTO = CellarMapSerializer.JsonToCellarMapFileDTO(cellarMapJson);
            _cellarMapInteractor = CellarMapInteractor.DTO_ToCellarMapInteractor(
                cellarMapFileDTO);
        }
    }
}
