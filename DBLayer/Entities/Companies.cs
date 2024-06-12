using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Companies
{
    [Key]
    public Guid CompanieId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public int MinFunc { get; set; }
    
    [Required]
    public int MaxFunc { get; set; }
    
    [Required]
    public string Site { get; set; }
    
    public string UrlImage { get; set; }
}