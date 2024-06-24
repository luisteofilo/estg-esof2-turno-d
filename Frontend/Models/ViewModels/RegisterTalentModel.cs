using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.ViewModels;

public class RegisterTalentModel
{
    [Required(ErrorMessage = "Please provide your name.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Please provide an email.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Please provide a password.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
    
    [Required(ErrorMessage = "Please provide a phone number.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "Please provide an address.")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Please provide a city.")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Please provide a country.")]
    public string Country { get; set; }
    
    [Required(ErrorMessage = "Please provide a postal code.")]
    public string PostalCode { get; set; }
    
    public string AreaOfInterest { get; set; }
}