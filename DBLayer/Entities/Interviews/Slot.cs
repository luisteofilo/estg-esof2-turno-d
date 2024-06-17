using System;
using System.ComponentModel.DataAnnotations;
namespace ESOF.WebApp.DBLayer.Entities;

public class Slot
{
    public Guid InterviewId { get; set; }
    public Guid InterviewerId { get; set; }
    [Key]
    public Guid SlotId { get; set; }
    
    [Required]
    public bool Ocupied { get; set; }
    
    [Required]
    public DateTime DateHourStart { get; set; } 
        
    [Required]
    public DateTime DateHourEnd { get; set; } 
        
    [Required]
    public Interviewer Interviewer { get; set; }
}