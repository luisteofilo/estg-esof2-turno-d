using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESOF.WebApp.DBLayer.Entities.FAQ;

public class Question
{
    [Key]
    public Guid QuestionId { get; set; }
    
    [Required]
    public string QuestionText { get; set; }
    
    [NotMapped]
    public Answer? VerifiedAnswer { get; set; }
    
    public User? Verifier { get; set; }
    
    [Required]
    public Job Job { get; set; }

    public ICollection<Answer>? Answers { get; set; }
}