using ESOF.WebApp.DBLayer.Entities;
using System.Linq;

namespace Common.Dtos.Taxonomias
{
    public static class DtoConversionTaxonomias
    {
        public static VerticalDto ToDto(this Vertical vertical)
        {
            return new VerticalDto
            {
                VerticalId = vertical.VerticalId,
                VerticalName = vertical.VerticalName,
                RoleVerticals = vertical.Roles_verticals?.Select(rv => rv.ToDto()).ToList(),
                VerticalUsers = vertical.VerticalsUsers?.Select(vu => vu.ToDto()).ToList()
            };
        }

        public static RoleVerticalDto ToDto(this Role_verticals roleVertical)
        {
            return new RoleVerticalDto
            {
                RoleVerticalId = roleVertical.Role_verticalsId,
                RoleVerticalName = roleVertical.Role_verticalsName,
                SkillVerticals = roleVertical.Skills_verticals?.Select(sv => sv.ToDto()).ToList()
            };
        }

        public static SkillVerticalDto ToDto(this skil_veticals skillVertical)
        {
            return new SkillVerticalDto
            {
                SkillVerticalId = skillVertical.skil_veticalsId,
                SkillVerticalName = skillVertical.skil_veticalsName,
                SkillVerticalExperience = skillVertical.skil_veticalsExperiencia
            };
        }

        public static VerticalUserDto ToDto(this verticalsUser verticalUser)
        {
            return new VerticalUserDto
            {
                UserId = verticalUser.UserId,
               
            };
        }

        public static Vertical ToEntity(this VerticalDto verticalDto)
        {
            return new Vertical
            {
                VerticalId = verticalDto.VerticalId,
                VerticalName = verticalDto.VerticalName,
                Roles_verticals = verticalDto.RoleVerticals?.Select(rv => rv.ToEntity()).ToList(),
                VerticalsUsers = verticalDto.VerticalUsers?.Select(vu => vu.ToEntity()).ToList()
            };
        }

        public static Role_verticals ToEntity(this RoleVerticalDto roleVerticalDto)
        {
            return new Role_verticals()
            {
               Role_verticalsId = roleVerticalDto.RoleVerticalId,
                Role_verticalsName = roleVerticalDto.RoleVerticalName,
                Skills_verticals = roleVerticalDto.SkillVerticals?.Select(sv => sv.ToEntity()).ToList()
            };
        }

        public static skil_veticals ToEntity(this SkillVerticalDto skillVerticalDto)
        {
            return new skil_veticals()
            {
                skil_veticalsId= skillVerticalDto.SkillVerticalId,
                skil_veticalsName = skillVerticalDto.SkillVerticalName,
                skil_veticalsExperiencia = skillVerticalDto.SkillVerticalExperience
            };
        }

        public static verticalsUser ToEntity(this VerticalUserDto verticalUserDto)
        {
            return new verticalsUser()
            {
                UserId = verticalUserDto.UserId,
               
            };
        }
        }
    }

    public class VerticalDto
    {
        public Guid VerticalId { get; set; }
        public string VerticalName { get; set; }
        public List<RoleVerticalDto> RoleVerticals { get; set; }
        public List<VerticalUserDto> VerticalUsers { get; set; }
    }

    public class RoleVerticalDto
    {
        public Guid RoleVerticalId { get; set; }
        public string RoleVerticalName { get; set; }
        public List<SkillVerticalDto> SkillVerticals { get; set; }
    }

    public class SkillVerticalDto
    {
        public Guid SkillVerticalId { get; set; }
        public string SkillVerticalName { get; set; }
        public int SkillVerticalExperience { get; set; }
    }

    public class VerticalUserDto
    {
        public Guid UserId { get; set; }
        
    }