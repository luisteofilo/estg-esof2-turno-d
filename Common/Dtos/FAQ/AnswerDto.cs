namespace Common.Dtos.FAQ;

public class AnswerDto
{
    public Guid AnswerId { get; set; }
    public string AnswerText { get; set; }
    public string AuthorEmail { get; set; }
    
    public Guid QuestionId { get; set; }
}