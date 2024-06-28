using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESOF.WebApp.DBLayer.Entities.Emails;

public class EmailDto
{
    public int Id { get; set; }
    [Required]
    public string Subject { get; set; }
    [Required]
    public string Body { get; set; }
}