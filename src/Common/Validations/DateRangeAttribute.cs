using System.ComponentModel.DataAnnotations;

namespace Common.Validations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
public class DateRangeAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;
    public DateRangeAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;
        var currentValue = (DateTime)value;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            throw new ArgumentException("Comparison property not found");
        var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);
        if(currentValue > comparisonValue)
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} cannot be after {_comparisonProperty}.");
        return ValidationResult.Success;
    }
}
