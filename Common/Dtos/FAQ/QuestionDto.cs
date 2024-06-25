namespace Common.Dtos.FAQ;

public class QuestionDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string? VerifiedAnswer { get; set; }
    public Guid? Verifier { get; set; }
}
