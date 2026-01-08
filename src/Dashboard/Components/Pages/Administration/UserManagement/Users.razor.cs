using Common.Models.UserManagement;
using Common.Models.UserManagement.Dtos;
using Dashboard.Components.UI.Tables.Models;
using Dashboard.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Dashboard.Components.Pages.Administration.UserManagement;

public partial class Users
{
    #region Variable(s)
    [Inject]
    private IUserService UserService { get; set; }

    [SupplyParameterFromForm]
    private SearchUser? SearchCriteria { get; set; }
    private void Reset() => SearchCriteria = new();
    private IEnumerable<User> users { get; set; } = Enumerable.Empty<User>();
    private bool isLoaded = false;

    //Table Variables
    private List<ColumnDefinition<User>> _userColumns = new();


    //Logging variables
    private DateTime startDate = DateTime.Now;
    private DateTime endDate = DateTime.Now;
    #endregion

    #region Method(s)
    protected override async Task OnInitializedAsync()
    {
        SearchCriteria ??= new();
        users = await UserService.GetAllUsersAsync();
        SetColumns();
        endDate = DateTime.Now;
        isLoaded = true;
    }
    private void Submit()
    {

    }

    private void ClickRow(User user)
    {
        Console.WriteLine("Row clicked");
    }
    private void ClickButton(User user)
    {
        Console.WriteLine("Button clicked");
    }
    private TimeSpan GetDuration()
    {
        return endDate - startDate;
    }
    
    #endregion
}
