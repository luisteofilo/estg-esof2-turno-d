﻿using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities.Interviews;

public class Candidate
{
    [Key]
    public Guid CandidateId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required] 
    public ICollection<Interview> Interviews { get; set; }

}