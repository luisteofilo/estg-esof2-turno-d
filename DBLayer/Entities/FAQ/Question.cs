using System.ComponentModel.DataAnnotations;
namespace ESOF.WebApp.DBLayer.Entities.FAQ;

public class Question
{
    [Key]
    public Guid QuestionId { get; set; }
    
    [Required]
    public string QuestionText { get; set; }
    
    public Answer VerifiedAnswer { get; set; }
    
    public User Verifier { get; set; }
    
    public ICollection<Answer> Answers { get; set; }
}