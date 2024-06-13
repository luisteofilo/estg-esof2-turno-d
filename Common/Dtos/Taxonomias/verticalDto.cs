using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Taxonomias;

public class verticalDto
{
    public string? VerticalName { get; set; }
    
    
    public List<Roles_verticalsDto> Roles_verticals { get; set; }
    
    
}