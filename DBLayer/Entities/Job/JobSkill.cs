﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESOF.WebApp.DBLayer.Entities;

public class JobSkill
{
    public Guid JobId { get; set; }
    public Job Job { get; set; }
    public Guid SkillId { get; set; }
    public Skill Skill { get; set; }
    
    [Required, DefaultValue(true)]
    public bool IsRequired { get; set; }
}