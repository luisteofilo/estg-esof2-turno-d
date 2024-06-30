using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Talent
{
    [Key]
    public Guid TalentId { get; set; }
    [Required]
    public string Name { get; set; }
    [Phone, Required]
    public string Phone { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public string AreaOfInterest { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid ProfileId { get; set; }
    
    public Profile Profile { get; set; }
    public User User { get; set; }
}