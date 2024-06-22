using ESOF.WebApp.DBLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Profile;
public interface IDashboardRepository
{
    Task<IEnumerable<ProfileSkillDto>> GetProfileSkillsAsync();
    
    Task<IEnumerable<Skill>> GetListOfSkillsAsync();
}