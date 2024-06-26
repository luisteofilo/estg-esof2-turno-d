using System;
using System.Collections.Generic;

namespace Common.Dtos.Taxonomias;

public class VerticalDto
{
    public Guid VerticalId { get; set; }
    public string VerticalName { get; set; }
    public List<RoleVerticalDto> RoleVerticals { get; set; }
    public List<VerticalUserDto> VerticalUsers { get; set; }
}