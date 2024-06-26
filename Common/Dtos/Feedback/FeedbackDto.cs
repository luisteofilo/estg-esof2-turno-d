namespace Common.Dtos.Feedback;
using ESOF.WebApp.DBLayer.Entities;

public class FeedbackDto
{
    public Guid FeedbackId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public Guid InterviewId { get; set; }
}