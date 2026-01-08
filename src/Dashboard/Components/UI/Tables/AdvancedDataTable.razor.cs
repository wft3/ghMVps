using Dashboard.Components.UI.Tables.Models;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace Dashboard.Components.UI.Tables;

public partial class AdvancedDataTable<TItem> where TItem : class
{
    #region Variable(s)

    #region ToolbarButton Variable(s)
    [Parameter]
    public bool CanSelectColumns { get; set; }
    [Parameter]
    public bool CanExport { get; set; }
    [Parameter]
    public bool CanRefresh { get; set; }

    #endregion
    [Parameter]
    public bool IsLoaded { get; set; }

    [Parameter]
    public IEnumerable<TItem>? Data { get; set; }

    [Parameter]
    public List<ColumnDefinition<TItem>>? Columns { get; set; }

    [Parameter]
    public RenderFragment? HeaderButtonContent { get; set; }

    #region Pagination Variable(s)
    #region Pagination
    [Parameter]
    public int PageSize { get; set; } = 20;

    [Parameter]
    public int CurrentPage { get; set; } = 1;
    private int totalPages = 1;
    private ICollection<TItem>? pagedItems;
    #endregion
    #endregion

    private List<ColumnDefinition<TItem>> _allColumns { get; set; } = new();
    private List<SortColumn<TItem>> _sortColumns = new();
    private IEnumerable<ColumnDefinition<TItem>> VisibleColumns => _allColumns.Where(c => c.IsVisible).OrderBy(c => c.Order);
    private string _columnSelectorVisible = string.Empty;

    private IEnumerable<TItem>? SortedData
    {
        get
        {
            if (Data == null) return Data;
            var query = Data.AsQueryable();
            if (!_sortColumns.Any()) return query;
            IOrderedQueryable<TItem>? orderedQuery = null;
            for (int i = 0; i < _sortColumns.Count; i++)
            {
                var sortColumn = _sortColumns[i];
                if (i == 0)
                {
                    orderedQuery = sortColumn.IsDescending
                        ? query.OrderByDescending(sortColumn.Column?.SortExpression!)
                        : query.OrderBy(sortColumn.Column?.SortExpression!);
                }
                else
                {
                    orderedQuery = sortColumn.IsDescending
                        ? orderedQuery!.ThenByDescending(sortColumn.Column?.SortExpression!)
                        : orderedQuery!.ThenBy(sortColumn.Column?.SortExpression!);
                }
            }
            return orderedQuery ?? query;
        }
    }
    #endregion

    #region Method(s)

    #region Pagination Method(s)
    private void UpdatePagedItems()
    {
        if (SortedData == null)
        {
            pagedItems = null;
            totalPages = 1;
            return;
        }

        totalPages = (int)Math.Ceiling((double)SortedData.Count() / PageSize);
        pagedItems = SortedData
         .Skip((CurrentPage - 1) * PageSize)
         .Take(PageSize)
         .ToList();

    }

    private void GoToPage(int selectedPage)
    {
        CurrentPage = selectedPage;
        UpdatePagedItems();
    }

    private void PreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            UpdatePagedItems();
        }
    }

    private void NextPage()
    {
        if (CurrentPage < totalPages)
        {
            CurrentPage++;
            UpdatePagedItems();
        }
    }
    #endregion

    protected override void OnParametersSet()
    {
        if (Columns != null && !_allColumns.Any())
        {
            _allColumns = Columns.Select((column, index) =>
            {
                column.Order = index;
                column.IsVisible = true;
                return column;
            }).ToList();
        }
        UpdatePagedItems();
    }

    private void HandleSort(ColumnDefinition<TItem> column)
    {
        var existingSort = _sortColumns.FirstOrDefault(s => s.Column == column);
        if (existingSort != null)
            existingSort.IsDescending = !existingSort.IsDescending;
        else
        {
            _sortColumns.Add(new SortColumn<TItem>
            {
                Column = column
            });
        }
        UpdatePagedItems();
    }
    private void RemoveSort(SortColumn<TItem> sortColumn)
    {
        _sortColumns.Remove(sortColumn);
    }
    private void ClearSort()
    {
        _sortColumns.Clear();
    }
    private void ToggleColumnSelector()
    {
        _columnSelectorVisible = string.IsNullOrEmpty(_columnSelectorVisible)
            ? "show"
            : string.Empty;
    }

    private void ToggleColumnVisibility(ColumnDefinition<TItem> column) => column.IsVisible = !column.IsVisible;

    private void MoveColumnUp(ColumnDefinition<TItem> column)
    {
        var currentIndex = _allColumns.IndexOf(column);
        if (currentIndex > 0)
        {
            _allColumns[currentIndex].Order = currentIndex - 1;
            _allColumns[currentIndex - 1].Order = currentIndex;
            _allColumns = _allColumns.OrderBy(c => c.Order).ToList();
        }
    }
    private void MoveColumnDown(ColumnDefinition<TItem> column)
    {
        var currentIndex = _allColumns.IndexOf(column);
        if (currentIndex < _allColumns.Count - 1)
        {
            _allColumns[currentIndex].Order = currentIndex + 1;
            _allColumns[currentIndex + 1].Order = currentIndex;
            _allColumns = _allColumns.OrderBy(c => c.Order).ToList();
        }
    }

    private string GetHeaderClass(ColumnDefinition<TItem> column)
    {
        var classes = new List<string> { "sortable" };
        if (_sortColumns.Any(s => s.Column == column))
            classes.Add("sorted");
        return string.Join(" ", classes);
    }
    #endregion
}

