using FluentValidation;

namespace Common.Dtos.Profile.Validators;

public class ExperienceDtoValidator : AbstractValidator<ExperienceDto>
{
    public ExperienceDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Experience Title is required")
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters");
                
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company Name is required")
            .MaximumLength(100).WithMessage("Description cannot exceed 100 characters");
                
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start Date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End Date is required")
            .GreaterThan(x => x.StartDate).WithMessage("End Date must be after Start Date");
                
        RuleFor(x => x.Description)
            .MaximumLength(300).WithMessage("Description cannot exceed 300 characters");
    }
}

