using System;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class Interviewer
{
    [Key]
    public Guid InterviewerId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required] 
    public ICollection<Interview> Interviews { get; set; }
    
    [Required] 
    public ICollection<Slot> Slots { get; set; }
}