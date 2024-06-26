using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities.FAQ;

public class Answer
{
    [Key]
    public Guid AnswerId { get; set; }
    
    [Required]
    public User Author { get; set; }
    
    [Required]
    public string AnswerText { get; set; }
    
    [Required]
    public Question Question { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public String AuthorEmail { get; set; }
}