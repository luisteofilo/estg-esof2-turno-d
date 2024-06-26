using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence;
using ESOF.WebApp.Scraper.Contracts;
using ESOF.WebApp.Scraper.Helpers;
using ESOF.WebApp.WebAPI.Errors;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Hangfire;
using Experience = ESOF.WebApp.DBLayer.Entities.Experience;

namespace ESOF.WebApp.WebAPI.Services;

public class ExternalProfileService(IProfileRepository _profileRepository,
                                    //IExperienceRepository _experienceRepository,
                                    //IEducationRepository _educationRepository,
                                    ProfileScraperFactory _scraper,
                                    IUnitOfWork _unitOfWork)
{
    
    [AutomaticRetry(Attempts = 1)]
    public async Task<Profile> AddExternalProfileAsync(Guid profileId,string url)
    {
        
        
        url = url.Trim();

        if (string.IsNullOrWhiteSpace(url))
        {
            throw new NullUrlException();
        }

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new FormatUrlException();

        }

        try
        {
            IScraper<ProfileResult> scraper = _scraper.CreateProfileScraper(new(url));
            
            var request = await scraper.Handle(url);

            var profile = await _profileRepository.GetProfileAsync(profileId);
            
            profile.FirstName = request.FirstName;
            profile.LastName = request.LastName;
            profile.Bio = request.Bio;
            profile.Location = request.Location;
            
            /*var tempExps = ConvertExperiencesFromRequest(profile, request);
            foreach (var exp in  tempExps)
            {
                profile.Experiences.Add(exp);
                await _experienceRepository.AddExperienceAsync(exp);
            }
            
            var tempSkills = ConvertProfileSkillsFromRequest(profile, request);
            foreach (var skl in tempSkills)
            {
                profile.ProfileSkills.Add(skl);
            }
            
            var tempEdus = ConvertEducationsFromRequest(profile, request);
            foreach (var edu in tempEdus)
            {
                profile.Educations.Add(edu);
                await _educationRepository.AddEducationAsync(edu);
            }*/
            
            
            await _profileRepository.UpdateProfileAsync(profile);
            await _unitOfWork.SaveChangesAsync();

            return profile;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /*private List<Experience> ConvertExperiencesFromRequest(Profile profile,ProfileResult request)
    {
        var experiences = new List<Experience>();

            foreach (var exp in request.experiences) {
                var experience = new Experience
                {
                    ProfileId = profile.ProfileId,
                    Name = exp.Name,
                    CompanyName = exp.CompanyName,
                    StartDate = exp.StartDate,
                    EndDate = exp.EndDate,
                    Description = exp.Description,
                    Profile = profile
                };
                
                experiences.Add(experience);
            }

            return experiences;
    }
    
    private List<ProfileSkill> ConvertProfileSkillsFromRequest(Profile profile, ProfileResult request)
    {
        var profileSkills = new List<ProfileSkill>();
        
            foreach (var skl in request.skills)
            {
                var skill = new Skill
                {
                    Name = skl.Name
                };

                var profileSkill = new ProfileSkill
                {
                    ProfileId = profile.ProfileId,
                    Profile = profile,
                    SkillId = skill.SkillId,
                    Skill = skill
                };
            
                profileSkills.Add(profileSkill);
            }

            return profileSkills;
    }
    
    private List<Education> ConvertEducationsFromRequest(Profile profile, ProfileResult request)
    {
        var educations = new List<Education>();
            foreach (var edu in request.educations)
            {
                var education = new Education
                {
                    ProfileId = profile.ProfileId,
                    Name = edu.Name,
                    SchoolName = edu.SchoolName,
                    StartDate = edu.StartDate,
                    EndDate = edu.EndDate,
                    Profile = profile
                };
                
                educations.Add(education);
            }

            return educations;
    }*/
    
    
}