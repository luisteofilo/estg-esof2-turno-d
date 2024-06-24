using System.ComponentModel.DataAnnotations;

namespace Frontend.Models.ViewModels;

public class RegisterAdminViewModel
{
    [Required (AllowEmptyStrings = false, ErrorMessage = "Please provide email")]
    public string Email { get; set; }
    
    [Required (AllowEmptyStrings = false, ErrorMessage = "Please provide password")]
    public string Password { get; set; }
    
}