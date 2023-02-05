using System.IO;
using IUP.Toolkits.CellarMaps.Serialization;
using UnityEditor;
using UnityEngine;
using DTO = IUP.Toolkits.CellarMaps.Serialization.DTO;

namespace IUP.Toolkits.CellarMaps
{
    /// <summary>
    /// Ассет клеточной карты.
    /// </summary>
    public class CellarMapAsset : ScriptableObject
    {
        /// <summary>
        /// Клеточная карта.
        /// </summary>
        public IReadOnlyCellarMap CellarMap { get; private set; }

        [SerializeField] private DTO.CellarMap _cellarMapDTO;

        private void OnEnable()
        {
            LoadAsset();
        }

        /// <summary>
        /// Загружает ассет из связанного с ним файла клеточной карты, инициализируя свойство CellarMap.
        /// </summary>
        private void LoadAsset()
        {
#if UNITY_EDITOR
            string assetPath = AssetDatabase.GetAssetPath(this);
            /* Данное условие срабатывает в первый раз, когда ассет только создан;
             * в этом случае AssetDatabase.GetAssetPath(this) вернёт null.*/
            if (assetPath == null)
            {
                return;
            }
            string cellarMapJson = File.ReadAllText(assetPath);
            _cellarMapDTO = CellarMapSerializer.JsonToCellarMapFileDTO(cellarMapJson).cellar_map;
#endif
            CellarMap = CellarMapSerializer.DTO_ToCellarMap(_cellarMapDTO);
        }

        /// <summary>
        /// Создаёт ассет клеточной карты.
        /// </summary>
        public static CellarMapAsset CreateAsset()
        {
            return CreateInstance<CellarMapAsset>();
        }
    }
}
