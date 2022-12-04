namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления индикатора типа клетки клеточной карты.
    /// </summary>
    public interface ICellTypeIndicator
    {
        /// <summary>
        /// Обновляет представление индикатора типа клетки клеточной карты.
        /// </summary>
        /// <param name="viewData">Визуальные данные типа клетки клеточной карты.</param>
        public void SetViewData(ICellTypeViewData viewData);
    }
}
