using System;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class Interview
    {
        [Key]
        public Guid InterviewId { get; set; }
        
        [Required]
        public DateTime Date { get; set; } 
        
        [Required]
        public TimeSpan Hour { get; set; } 
        
        [Required]
        public InterviewState InterviewState { get; set; } 
        
        [Required]
        public string Location { get; set; }
    }
}