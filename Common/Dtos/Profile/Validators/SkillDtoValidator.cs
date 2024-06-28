using FluentValidation;

namespace Common.Dtos.Profile.Validators;

public class SkillDtoValidator : AbstractValidator<SkillDto>
{
    public SkillDtoValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(30).WithMessage("Name cannot exceed 30 characters");
    }
}