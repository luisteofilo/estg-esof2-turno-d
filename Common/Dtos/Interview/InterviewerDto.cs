using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Interview;
public class InterviewerDto
{
    public Guid InterviewerId { get; set; }
    public string Name { get; set; }
}