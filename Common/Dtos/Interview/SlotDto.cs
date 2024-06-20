
using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Interview;
public class SlotDto
{
    public Guid InterviewId { get; set; }
    public Guid InterviewerId { get; set; }
    public Guid SlotId { get; set; }
    public bool Ocupied { get; set; }
    public DateTime DateHourStart { get; set; } 
    public DateTime DateHourEnd { get; set; } 
}