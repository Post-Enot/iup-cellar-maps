namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления индикатора слоя клеточной карты.
    /// </summary>
    public interface ILayerIndicator
    {
        /// <summary>
        /// Обновляет представление индикатора слоя клеточной карты.
        /// </summary>
        /// <param name="viewData">Визуальные данные слоя клеточной карты.</param>
        public void SetViewData(ILayerViewData viewData);
    }
}
