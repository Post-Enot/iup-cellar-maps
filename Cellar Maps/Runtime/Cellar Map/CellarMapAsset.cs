using IUP.Toolkits.CellarMaps.Serialization;
using System;
using System.IO;
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
        public ICellarMap CellarMap { get; private set; }
        /// <summary>
        /// Путь к файлу клеточной карты, связанный с данным ассетом.
        /// </summary>
        public string FilePath => _filePath;
        /// <summary>
        /// True, если ассет связан с файлом клеточной карты.
        /// </summary>
        public bool IsAssetBound => _isAssetBound;
        /// <summary>
        /// True, если ассет был загружен с помощью метода LoadAsset.
        /// </summary>
        public bool IsAssetLoaded { get; private set; }

        [SerializeField] private string _filePath;
        [SerializeField] private bool _isAssetBound;
        private DTO.CellarMapFile _cellarMapFileDTO;

        /// <summary>
        /// Загружает ассет из связанного с ним файла клеточной карты, инициализируя свойство CellarMap.
        /// </summary>
        public void LoadAsset()
        {
            _cellarMapFileDTO = LoadCellarMapFileDTO();
            CellarMap = CellarMaps.CellarMap.DTO_ToCellarMap(_cellarMapFileDTO.cellar_map);
            IsAssetLoaded = true;
        }

        /// <summary>
        /// Сохраняет ассет в связанный с ним файл клеточной карты.
        /// </summary>
        public void SaveAsset()
        {
            if (!IsAssetLoaded)
            {
                throw new InvalidOperationException("Сохранение ассета невозможно, т.к. ассет не был загружен.");
            }
            _cellarMapFileDTO.cellar_map = CellarMap.ToDTO();
            string cellarMapJson = CellarMapSerializer.CellarMapFileDTO_ToJson(_cellarMapFileDTO);
            File.WriteAllText(_filePath, cellarMapJson);
        }

        private DTO.CellarMapFile LoadCellarMapFileDTO()
        {
            if (!IsAssetBound)
            {
                throw new InvalidOperationException(
                    "Ассет не связан с файлом клеточной карты: скорее всего это вызвано тем, что он был создан с помощью метода ScriptableObject.CreateInstance. Для создания ассета используйте метод CellarMapAsset.CreateAsset.");
            }
            string cellarMapJson = File.ReadAllText(_filePath);
            return CellarMapSerializer.JsonToCellarMapFileDTO(cellarMapJson);
        }

        /// <summary>
        /// Создаёт и связывает ассет с json-файлом, используемым для сериализации и десериализации ассета.
        /// </summary>
        /// <param name="cellarMapFilePath">путь до json-файла клеточной карты.</param>
        public static CellarMapAsset CreateAsset(string cellarMapFilePath)
        {
            var asset = CreateInstance<CellarMapAsset>();
            asset.BindWithJson_File(cellarMapFilePath);
            return asset;
        }

        /// <summary>
        /// Связывает ассет с json-файлом, используемым для сериализации и десериализации ассета.
        /// </summary>
        /// <param name="cellarMapFilePath">путь до json-файла клеточной карты.</param>
        private void BindWithJson_File(string cellarMapFilePath)
        {
            _filePath = cellarMapFilePath;
            _isAssetBound = true;
        }
    }
}
