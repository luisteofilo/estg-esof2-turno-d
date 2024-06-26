using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Interview
{
    [Key]
    public Guid InterviewId;
    
    public Feedback Feedback;
}