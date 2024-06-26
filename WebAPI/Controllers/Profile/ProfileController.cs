using Common.Dtos.Profile;
using ESOF.WebApp.WebAPI.Contracts.Job;
using ESOF.WebApp.WebAPI.Errors;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.WebAPI.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace ESOF.WebApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(
    IProfileRepository _profileRepository,
    ISkillRepository _skillRepository,
    IEducationRepository _educationRepository,
    IExperienceRepository _experienceRepository,
    ExternalProfileService _externalProfileService): ControllerBase
{

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProfileDto>))]
    public async Task<IActionResult> GetProfiles()
    {
        try
        {
            var profiles = await _profileRepository.GetProfilesAsync();
            var profilesDto = profiles.ProfilesConvertToDto();
            return Ok(profilesDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profiles: {ex.Message}");
        }
    }

    [HttpGet("{profileId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProfileDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProfile(Guid profileId)
    {
        try
        {
            if (!await _profileRepository.ProfileExistsAsync(profileId))
            {
                return NotFound();
            }

            var profile = await _profileRepository.GetProfileAsync(profileId);
            var profileDto = profile.ProfileConvertToDto();

            return Ok(profileDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ProfileDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileDto profileDto)
    {
        try
        {
            if (profileDto == null)
            {
                return BadRequest("Profile details are null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = profileDto.DtoConvertToProfile();
            await _profileRepository.AddProfileAsync(profile);

            var createdProfileDto = profile.ProfileConvertToDto();

            return CreatedAtAction(nameof(GetProfile), new { profileId = createdProfileDto.ProfileId }, createdProfileDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating profile: {ex.Message}");
        }
    }

    [HttpPut("{profileId:guid}")]
    [ProducesResponseType(204, Type = typeof(ProfileDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateProfile(Guid profileId, [FromBody] ProfileDto updatedProfileDto)
    {
        try
        {
            if (updatedProfileDto == null || updatedProfileDto.ProfileId != profileId)
            {
                return BadRequest(ModelState);
            }

            if (!await _profileRepository.ProfileExistsAsync(profileId))
            {
                return NotFound();
            }

            var updatedProfile = updatedProfileDto.DtoConvertToProfile();
            await _profileRepository.UpdateProfileAsync(updatedProfile);

            return Ok(updatedProfileDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating profile: {ex.Message}");
        }
    }

    [HttpDelete("{profileId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteProfile(Guid profileId)
    {
        try
        {
            if (!await _profileRepository.ProfileExistsAsync(profileId))
            {
                return NotFound();
            }

            await _profileRepository.DeleteProfileAsync(profileId);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting profile: {ex.Message}");
        }
    }
    
    [HttpPost("upload-image"), DisableRequestSizeLimit]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UploadImage([FromForm] AvatarFormDto form)
    {
        try
        {
            if (form.File == null || form.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var folderName = Path.Combine("Resources", "Avatars");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            
            var fileExtension = Path.GetExtension(form.File.FileName);
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            var fileName = $"{form.ProfileId}_{timestamp}{fileExtension}";
            var fullPath = Path.Combine(pathToSave, fileName);
            var accessPath = Path.Combine(folderName, fileName);
            
            var profile = await _profileRepository.GetProfileAsync(form.ProfileId);
            if (!string.IsNullOrEmpty(profile.Avatar))
            {
                var previousAvatarFullPath = Path.Combine(Directory.GetCurrentDirectory(), profile.Avatar);
                if (System.IO.File.Exists(previousAvatarFullPath))
                {
                    System.IO.File.Delete(previousAvatarFullPath);
                }
            }
            
            await using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await form.File.CopyToAsync(stream);
            }
            
            await _profileRepository.UpdateProfileAvatarAsync(form.ProfileId, accessPath);
            return Ok(accessPath);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error storing profile avatar: {ex.Message}");
        }
    }
    
    [HttpGet("url/{profileUrl}")]
    [ProducesResponseType(200, Type = typeof(ProfileDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProfileByUrl(string profileUrl)
    {
        try
        {
            var profile = await _profileRepository.GetProfileByUrlAsync(profileUrl);
            var profileDto = profile.ProfileConvertToDto();

            return Ok(profileDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile: {ex.Message}");
        }
    }

    // Endpoints para Skill
    
    [HttpGet("skills")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SkillDto>))]
    public async Task<IActionResult> GetSkills()
    {
        try
        {
            var skills = await _skillRepository.GetSkillsAsync();
            var skillsDto = skills.SkillsConvertToDto();
            return Ok(skillsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving skills: {ex.Message}");
        }
    }
    
    [HttpPost("skills")]
    [ProducesResponseType(201, Type = typeof(SkillDto))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateSkill([FromBody] SkillDto skillDto)
    {
        try
        {
            if (skillDto == null)
            {
                return BadRequest();
            }

            var newSkill = await _skillRepository.CreateSkillAsync(skillDto.DtoConvertToSkill());
            var newSkillDto = newSkill.SkillConvertToDto();

            return CreatedAtAction(nameof(GetSkillById), new { skillId = newSkillDto.SkillId }, newSkillDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating skill: {ex.Message}");
        }
    }

    [HttpGet("skills/{skillId:guid}")]
    [ProducesResponseType(200, Type = typeof(SkillDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetSkillById(Guid skillId)
    {
        try
        {
            var skill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skill == null)
            {
                return NotFound();
            }

            var skillDto = skill.SkillConvertToDto();
            return Ok(skillDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving skill: {ex.Message}");
        }
    }

    [HttpPut("skills/{skillId:guid}")]
    [ProducesResponseType(200, Type = typeof(SkillDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateSkill(Guid skillId, [FromBody] SkillDto updatedSkillDto)
    {
        try
        {
            if (updatedSkillDto == null || updatedSkillDto.SkillId != skillId)
            {
                return BadRequest();
            }

            var existingSkill = await _skillRepository.GetSkillByIdAsync(skillId);
            
            if (existingSkill == null)
            {
                return NotFound();
            }

            existingSkill.Name = updatedSkillDto.Name;
            await _skillRepository.UpdateSkillAsync(existingSkill);

            return Ok(updatedSkillDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating skill: {ex.Message}");
        }
    }

    [HttpDelete("skills/{skillId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteSkill(Guid skillId)
    {
        try
        {
            var skill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skill == null)
            {
                return NotFound();
            }

            await _skillRepository.DeleteSkillAsync(skillId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting skill: {ex.Message}");
        }
    }

    [HttpGet("{profileId:guid}/skills")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SkillDto>))]
    public async Task<IActionResult> GetProfileSkills(Guid profileId)
    {
        try
        {
            var skills = await _skillRepository.GetProfileSkillsAsync(profileId);
            var skillsDto = skills.SkillsConvertToDto();
            return Ok(skillsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile skills: {ex.Message}");
        }
    }

    [HttpPost("{profileId:guid}/skills/{skillId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddProfileSkill(Guid profileId, Guid skillId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileExists = await _profileRepository.ProfileExistsAsync(profileId);
            var skillExists = await _skillRepository.SkillExistsAsync(skillId);

            if (!profileExists || !skillExists)
            {
                return NotFound("Profile or Skill not found.");
            }

            await _skillRepository.AddSkillToProfileAsync(skillId, profileId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding skill to profile: {ex.Message}");
        }
    }

    [HttpDelete("{profileId:guid}/skills/{skillId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteProfileSkill(Guid profileId, Guid skillId)
    {
        try
        {
            var profileExists = await _profileRepository.ProfileExistsAsync(profileId);
            var skillExists = await _skillRepository.SkillExistsAsync(skillId);

            if (!profileExists || !skillExists)
            {
                return NotFound("Profile or Skill not found.");
            }

            await _skillRepository.RemoveSkillFromProfileAsync(skillId, profileId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting skill from profile: {ex.Message}");
        }
    }


    // Endpoints para Education

    [HttpGet("{profileId:guid}/educations")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<EducationDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProfileEducations(Guid profileId)
    {
        try
        {
            var existProfile = await _profileRepository.ProfileExistsAsync(profileId);
            
            if (!existProfile)
            {
                return NotFound();
            }

            var educations = await _educationRepository.GetProfileEducationsAsync(profileId);
            var educationsDto = educations.EducationsConvertToDto();
            
            return Ok(educationsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile educations: {ex.Message}");
        }
    }

    [HttpPost("{profileId:guid}/educations")]
    [ProducesResponseType(201, Type = typeof(EducationDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateProfileEducation(Guid profileId, [FromBody] EducationDto educationDto)
    {
        try
        {
            if (educationDto == null)
            {
                return BadRequest("Education details are null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _profileRepository.GetProfileAsync(profileId);
            if (profile == null)
            {
                return NotFound("Profile not found.");
            }
            
            var education = educationDto.DtoToEducation();
            education.ProfileId = profileId;
            
            await _educationRepository.AddEducationAsync(education);
            var createdEducationDto = education.EducationConvertToDto();

            return Ok(createdEducationDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating profile education: {ex.Message}");
        }
    }

    [HttpPut("{profileId:guid}/educations/{educationId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> UpdateProfileEducation(Guid profileId, Guid educationId, [FromBody] EducationDto updatedEducationDto)
    {
        try
        {
            if (updatedEducationDto == null || updatedEducationDto.EducationId != educationId)
            {
                return BadRequest(ModelState);
            }

            if (!await _educationRepository.EducationExistsAsync(educationId))
            {
                return NotFound();
            }

            var profile = await _profileRepository.GetProfileAsync(profileId);
            if (profile == null || profileId != updatedEducationDto.ProfileId)
            {
                return Unauthorized();
            }

            var updatedEducation = updatedEducationDto.DtoToEducation();
            await _educationRepository.UpdateEducationAsync(updatedEducation);

            return Ok(updatedEducationDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating profile education: {ex.Message}");
        }
    }

    [HttpDelete("{profileId:guid}/educations/{educationId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> DeleteProfileEducation(Guid profileId, Guid educationId)
    {
        try
        {
            var profile = await _profileRepository.GetProfileAsync(profileId);
            var education = await _educationRepository.GetEducationAsync(educationId);
            
            if (profile == null || education == null)
            {
                return NotFound();
            }
            
            if (profileId != education.ProfileId)
            {
                return Unauthorized();
            }
            
            await _educationRepository.DeleteProfileEducationAsync(educationId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting profile education: {ex.Message}");
        }
    }

    // Endpoints para Experience

    [HttpGet("{profileId:guid}/experiences")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ExperienceDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProfileExperiences(Guid profileId)
    {
        try
        {
            var existProfile = await _profileRepository.ProfileExistsAsync(profileId);
            if (!existProfile)
            {
                return NotFound();
            }

            var experiences = await _experienceRepository.GetProfileExperiencesAsync(profileId);
            var experiencesDto = experiences.ExperiencesConvertToDto();

            return Ok(experiencesDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving profile experiences: {ex.Message}");
        }
    }

    [HttpPost("{profileId:guid}/experiences")]
    [ProducesResponseType(200, Type = typeof(ExperienceDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> CreateProfileExperience(Guid profileId, [FromBody] ExperienceDto experienceDto)
    {
        try
        {
            if (experienceDto == null)
            {
                return BadRequest("Experience details are null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _profileRepository.GetProfileAsync(profileId);
            if (profile == null)
            {
                return NotFound("Profile not found.");
            }

            var experience = experienceDto.DtoToExperience();
            experience.ProfileId = profileId;
            
            await _experienceRepository.AddExperienceAsync(experience);
            var createdExperienceDto = experience.ExperienceConvertToDto();

            return Ok(createdExperienceDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating profile experience: {ex.Message}");
        }
    }

    [HttpPut("{profileId:guid}/experiences/{experienceId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> UpdateProfileExperience(Guid profileId, Guid experienceId, [FromBody] ExperienceDto updatedExperienceDto)
    {
        try
        {
            if (updatedExperienceDto == null || updatedExperienceDto.ExperienceId != experienceId)
            {
                return BadRequest(ModelState);
            }

            if (!await _experienceRepository.ExperienceExistsAsync(experienceId))
            {
                return NotFound();
            }

            var profile = await _profileRepository.GetProfileAsync(profileId);
            if (profile == null || profileId != updatedExperienceDto.ProfileId)
            {
                return Unauthorized();
            }

            var updatedExperience = updatedExperienceDto.DtoToExperience();
            await _experienceRepository.UpdateExperienceAsync(updatedExperience);
            return Ok(updatedExperienceDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating profile experience: {ex.Message}");
        }
    }

    [HttpDelete("{profileId:guid}/experiences/{experienceId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> DeleteProfileExperience(Guid profileId, Guid experienceId)
    {
        try
        {
            var profile = await _profileRepository.GetProfileAsync(profileId);
            var experience = await _experienceRepository.GetExperienceAsync(experienceId);
            
            if (profile == null || experience == null)
            {
                return NotFound();
            }
            
            if (profileId != experience.ProfileId)
            {
                return Unauthorized();
            }
            
            await _experienceRepository.DeleteExperienceAsync(experienceId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting profile experience: {ex.Message}");
        }
    }
    
    [HttpPost("{profileId:guid}/import")]
    public async Task<IActionResult> ImportProfile(Guid profileId,[FromBody] string url)
    {
        try
        {
            var profile = await _externalProfileService.AddExternalProfileAsync(profileId, url);

            var updatedProfile = profile.ProfileConvertToDto();
            return Ok(updatedProfile);
            
            
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting profile experience: {ex.Message}");
        }
    }
        
}
    