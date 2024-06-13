namespace Common.Dtos.Taxonomias;
using ESOF.WebApp.DBLayer.Entities;

public static class DToconversion_taxonomias {
    
    public static verticalDto ToDto(this Vertical pro)
    {
        return new verticalDto()
        {
            VerticalName = pro.VerticalName,
            Roles_verticals = pro.Roles_verticals.Select(exp => new Roles_verticalsDto()
            {
                Role_verticalsName = exp.Role_verticalsName,
                Skills_verticals = exp.Skills_verticals.Select(edu => new skills_verticalsDto()
                {
                    skil_veticalsName= edu.skil_veticalsName,
                    skil_veticalsExperiencia = edu.skil_veticalsExperiencia
                }).ToList()
            }).ToList(),
         
        };
    }
}