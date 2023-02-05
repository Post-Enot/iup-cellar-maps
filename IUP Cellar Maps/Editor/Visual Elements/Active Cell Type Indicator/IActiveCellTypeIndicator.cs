namespace IUP.Toolkits.CellarMaps.Editor.UI
{
    /// <summary>
    /// Интерфейс представления активного типа клетки клеточной карты.
    /// </summary>
    public interface IActiveCellTypeIndicator
    {
        /// <summary>
        /// Обновляет представление активного типа клетки клеточной карты.
        /// </summary>
        /// <param name="viewData">Визуальные данные типа клетки клеточной карты.</param>
        public void SetViewData(ICellTypeViewData viewData);
    }
}
