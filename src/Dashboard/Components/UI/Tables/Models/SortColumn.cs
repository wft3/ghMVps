namespace Dashboard.Components.UI.Tables.Models;

public class SortColumn<T> where T : class
{
    public ColumnDefinition<T>? Column { get; set; }
    public bool IsDescending { get; set; }
}
