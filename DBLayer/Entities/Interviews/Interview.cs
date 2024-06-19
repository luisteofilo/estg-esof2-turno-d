using System;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Interview
    {
        [Required]
        public Guid CandidateId { get; set; }
        [Required]
        public Guid InterviewerId { get; set; }
        [Required]
        public Guid SlotId { get; set; }
        [Key]
        public Guid InterviewId { get; set; }
        
        [Required]
        public InterviewState InterviewState { get; set; } 
                
        [Required]
        public string Location { get; set; }
        
        [Required]
        public DateTime DateHourStart { get; set; } 
        
        [Required]
        public DateTime DateHourEnd { get; set; } 
        
        public Candidate Candidate { get; set; }
        
        public Interviewer Interviewer { get; set; }
    }
}

