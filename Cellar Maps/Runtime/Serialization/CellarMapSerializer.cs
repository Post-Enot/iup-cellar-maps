using IUP.Toolkits.CellarMaps.Serialization.DTO;
using UnityEngine;

namespace IUP.Toolkits.CellarMaps.Serialization
{
    /// <summary>
    /// Класс с набором статичных методов для сериализации и десериализации клеточных карт.
    /// </summary>
    public static class CellarMapSerializer
    {
        /// <summary>
        /// Последняя версия формата данных клеточной карты.
        /// </summary>
        public const int LastDataFormatVersion = 0;

        public static CellarMapFile JsonToCellarMapFileDTO(string cellarMapJson)
        {
            return JsonUtility.FromJson<CellarMapFile>(cellarMapJson);
        }

        public static string CellarMapFileDTO_ToJson(CellarMapFile cellarMapFileDTO)
        {
            return JsonUtility.ToJson(cellarMapFileDTO, true);
        }
    }
}
