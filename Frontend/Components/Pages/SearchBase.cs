
using Common.Dtos.Profile;

using Frontend.Services;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;
namespace Frontend.Components.Pages;

public class SearchBase : ComponentBase
{
    [Inject] public IProfileService ProfileService { get; set; }

    public IEnumerable<ProfileDto> Profiles { get; set; }
    protected IEnumerable<SkillDto> Skills { get; set; }

    protected override async Task OnInitializedAsync()
    {

         Profiles = await ProfileService.GetProfiles();
         Skills = await ProfileService.GetSkills();

    }
}




    


