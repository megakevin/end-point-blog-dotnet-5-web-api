using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace VehicleQuotes.Validation
{
    public class VehicleBodyTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var dbContext = validationContext.GetService(typeof(VehicleQuotesContext)) as VehicleQuotesContext;

            var bodyTypes = dbContext.BodyTypes.Select(bt => bt.Name).ToList();

            if (!bodyTypes.Contains(value))
            {
                var allowed = String.Join(", ", bodyTypes);
                return new ValidationResult(
                    $"Invalid vehicle body type {value}. Allowed values are {allowed}."
                );
            }

            return ValidationResult.Success;
        }
    }
}
