
using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Interview;
public class InterviewDto
{
    public Guid InterviewId { get; set; }
    public Guid CandidateId { get; set; }
    public Guid InterviewerId { get; set; }
    public Guid SlotId { get; set; }
    public InterviewState InterviewState { get; set; }
    public string Location { get; set; }
    public DateTime DateHourStart { get; set; }
    public DateTime DateHourEnd { get; set; }
}