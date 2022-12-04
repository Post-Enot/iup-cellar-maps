namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления индикатора активного слоя клеточной карты.
    /// </summary>
    public interface IActiveLayerIndicator
    {
        /// <summary>
        /// Обновляет представление индикатора слоя клеточной карты.
        /// </summary>
        /// <param name="viewData">Визуальные данные слоя клеточной карты.</param>
        public void SetViewData(ILayerViewData viewData);
    }
}
