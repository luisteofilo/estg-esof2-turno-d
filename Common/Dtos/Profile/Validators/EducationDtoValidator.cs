using FluentValidation;

namespace Common.Dtos.Profile.Validators;

public class EducationDtoValidator : AbstractValidator<EducationDto>
{
    public EducationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Education Name is required")
            .MaximumLength(100).WithMessage("Education Name cannot exceed 100 characters");

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage("School Name is required")
            .MaximumLength(100).WithMessage("School Name cannot exceed 100 characters");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start Date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End Date is required")
            .GreaterThan(x => x.StartDate).WithMessage("End Date must be after Start Date");
    }
}