using System;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Interview
    {
        [Key]
        public Guid InterviewId { get; set; }
        
        [Required]
        public DateTime DateHour { get; set; } 
        
        [Required]
        [EnumDataType(typeof(InterviewState))]
        public string InterviewState { get; set; } 
        
        [Required]
        public string Location { get; set; }
    }
    public static class InterviewState
    {
        public static readonly string[] Values = new string[]
        {
            "Scheduled",
            "OnGoing",
            "Completed",
            "Canceled"
        };
    }
}

