using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Client
{
    [Key]
    public Guid ClientId { get; set; }
    
    public String Name { get; set; }
    
    public ICollection<Job> Jobs { get; set; }
}