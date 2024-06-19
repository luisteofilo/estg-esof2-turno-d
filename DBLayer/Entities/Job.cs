using System;
using System.Collections.Generic;

namespace ESOF.WebApp.DBLayer.Entities;

public class Job
{
    public Guid JobId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public ICollection<InterviewFeedback> InterviewFeedbacks { get; set; }
}