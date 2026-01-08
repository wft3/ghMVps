using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Dashboard.Components.UI.Tables.Models;

public class ColumnDefinition<T> where T : class
{
    #region Variable(s)
    public string PropertyName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Expression<Func<T, object>>? SortExpression { get; set; }
    public RenderFragment<T>? CellTemplate { get; set; }
    public bool IsVisible { get; set; }
    public int Order { get; set; }
    #endregion

    #region Method(s)
    public object? GetValue(T item)
    {
        var propertyInfo = typeof(T).GetProperty(PropertyName);
        return propertyInfo?.GetValue(item);
    }
    #endregion
}
