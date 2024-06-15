using System.ComponentModel.DataAnnotations;
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.DBLayer.Extensions;

public static class InterviewExtensions
{
    public class InterviewStateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !InterviewState.Values.Contains(value.ToString()))
            {
                return new ValidationResult("Invalid Interview State value.");
            }
            return ValidationResult.Success;
        }
    }
}