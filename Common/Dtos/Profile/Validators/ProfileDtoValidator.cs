using FluentValidation;

namespace Common.Dtos.Profile.Validators;

public class ProfileDtoValidator : AbstractValidator<ProfileDto>
{
    public ProfileDtoValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(30).WithMessage("First name cannot exceed 30 characters");
                
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(30).WithMessage("Last name cannot exceed 30 characters");
        
        RuleFor(p => p.Bio)
            .MaximumLength(300).WithMessage("Biography cannot exceed 300 characters");
        
        RuleFor(p => p.Location)
            .MaximumLength(30).WithMessage("Location cannot exceed 30 characters");
    }
}