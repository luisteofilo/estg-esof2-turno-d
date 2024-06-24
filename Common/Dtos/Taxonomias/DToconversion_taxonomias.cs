using ESOF.WebApp.DBLayer.Entities;
using System.Linq;

namespace Common.Dtos.Taxonomias
{
    public static class DtoConversionTaxonomias
    {
        public static VerticalDto ToDto(this Vertical vertical)
        {
            if (vertical == null)
                return null;

            return new VerticalDto
            {
                VerticalId = vertical.VerticalId,
                VerticalName = vertical.VerticalName,
                RolesVerticals = vertical.Roles_verticals?.Select(role => new RolesVerticalsDto
                {
                    RoleVerticalsName = role.Role_verticalsName,
                    SkillsVerticals = role.Skills_verticals?.Select(skill => new SkillsVerticalsDto
                    {
                        SkillVerticalsName = skill.skil_veticalsName,
                        SkillVerticalsExperiencia = skill.skil_veticalsExperiencia
                    }).ToList()
                }).ToList(),
                VerticalsUsers = vertical.VerticalsUsers?.Select(vu => new verticalsUserDto
                {
                    UserId = vu.UserId,
                    
                }).ToList()
                // Add other properties as needed
            };
        }
    }

    public class VerticalDto
    {
        public Guid VerticalId { get; set; }
        public string VerticalName { get; set; }
        public List<RolesVerticalsDto> RolesVerticals { get; set; }
        public List<verticalsUserDto> VerticalsUsers { get; set; }
    }

    public class RolesVerticalsDto
    {
        public string RoleVerticalsName { get; set; }
        public List<SkillsVerticalsDto> SkillsVerticals { get; set; }
    }

    public class SkillsVerticalsDto
    {
        public string SkillVerticalsName { get; set; }
        public int SkillVerticalsExperiencia { get; set; }
    }


    public class verticalsUserDto
    {
        public Guid UserId { get; set; }
    }
}