using System;

namespace IUP.Toolkits.CellarMaps.Serialization
{
    /// <summary>
    /// Исключение, вызываемое при невозможности распознать версию формата данных файла клеточной карты.
    /// </summary>
    public sealed class CantRecognizedVersionException : ArgumentException
    {
        /// <summary>
        /// Инициализирует поле версии формата данных файла клеточной карты.
        /// </summary>
        /// <param name="dataFormatVersion">Версия формата данных файла клеточной карты.</param>
        public CantRecognizedVersionException(int dataFormatVersion)
            : base(_message)
        {
            DataFormatVersion = dataFormatVersion;
        }

        /// <summary>
        /// Нераспознанная версия формата данных файла клеточной карты.
        /// </summary>
        public int DataFormatVersion { get; }

        private const string _message =
            "Ошибка сериализации: не распознана версия формата данных переданной строки.";
    }
}
