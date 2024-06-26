using System;
using System.Collections.Generic;

namespace Common.Dtos.Taxonomias;

public class RoleVerticalDto
{
    public Guid RoleVerticalId { get; set; }
    public string RoleVerticalName { get; set; }
    public List<SkillVerticalDto> SkillVerticals { get; set; }
}