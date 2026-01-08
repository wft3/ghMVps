using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace Dashboard.Components.UI.Tables.Models;

//public static class TableHelper
//{
//    public static List<AdvancedDataTable<T>.ColumnDefinition<T>> GenerateColumns<T>()
//    {
//        var columns = new List<AdvancedDataTable<T>.ColumnDefinition<T>>();
//        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

//        foreach (var prop in properties)
//        {
//            var parameter = Expression.Parameter(typeof(T), "x");
//            var property = Expression.Property(parameter, prop);
//            var conversion = Expression.Convert(property, typeof(object));
//            var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

//            var column = new AdvancedDataTable<T>.ColumnDefinition<T>
//            {
//                PropertyName = prop.Name,
//                DisplayName = FormatDisplayName(prop.Name),
//                SortExpression = lambda,
//                ValueGetter = item => prop.GetValue(item) ?? string.Empty
//            };

//            columns.Add(column);
//        }

//        return columns;
//    }

//    public static List<AdvancedDataTable<T>.ColumnDefinition<T>> GenerateColumnsWithTemplates<T>(
//        Dictionary<string, RenderFragment<T>>? customTemplates = null)
//    {
//        var columns = GenerateColumns<T>();

//        if (customTemplates != null)
//        {
//            foreach (var column in columns)
//            {
//                if (customTemplates.TryGetValue(column.PropertyName, out var template))
//                {
//                    column.CellTemplate = template;
//                    column.ValueGetter = null; // Clear ValueGetter when using template
//                }
//            }
//        }

//        return columns;
//    }

//    private static string FormatDisplayName(string propertyName)
//    {
//        // Convert PascalCase to Title Case with spaces
//        return System.Text.RegularExpressions.Regex.Replace(
//            propertyName,
//            "([A-Z])",
//            " $1"
//        ).Trim();
//    }
//}
