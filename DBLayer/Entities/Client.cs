using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Client
{
    [Key]
    public Guid ClientId { get; set; }
    [Required]
    public string CompanyName { get; set; }
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
    public string CompanyDescription { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}