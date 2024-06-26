namespace Common.Dtos.Feedback;
using ESOF.WebApp.DBLayer.Entities;

public static class DtoConversion
{
    public static FeedbackDto ToDto(this Feedback feedback)
    {
        return new FeedbackDto
        {
            FeedbackId = feedback.FeedbackId,
            Message = feedback.Message,
            Date = feedback.Date,
            InterviewId = feedback.InterviewId
        };
    }
}