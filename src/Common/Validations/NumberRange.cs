using System.ComponentModel.DataAnnotations;

namespace Common.Validations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
public class NumberRange: ValidationAttribute
{
    private readonly string _comparisonProperty;
    public NumberRange(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;
        var currentValue = Convert.ToDecimal(value);
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            throw new ArgumentException("Comparison property not found");
        var comparisonValue = Convert.ToDecimal(property.GetValue(validationContext.ObjectInstance));
        if(currentValue > comparisonValue)
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} cannot be greater than {_comparisonProperty}.");
        return ValidationResult.Success;
    }
}
