using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Dashboard.Components.UI.Checkboxes;

public partial class VisibleCheckbox
{
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public Expression<Func<bool>>? ValidationFor { get; set; }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out bool result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = bool.TryParse(value, out var parsedValue) && parsedValue;
        validationErrorMessage = null;
        return true;
    }
    private void ToggleValue() => CurrentValue = !CurrentValue;

    private void HandleKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == " " || e.Key == "Enter")
            ToggleValue();
    }
}
