namespace Common.Dtos.FAQ;

public class QuestionDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string? VerifiedAnswer { get; set; }
    public Guid? Verifier { get; set; }
    
    public Guid JobId { get; set; }
    
    public IEnumerable<AnswerDto> Answers { get; set; }
}
