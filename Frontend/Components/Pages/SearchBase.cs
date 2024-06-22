
using Common.Dtos.Profile;

using Frontend.Services;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;
namespace Frontend.Components.Pages;

public class SearchBase : ComponentBase
{
    [Inject] public ISearchService SearchService { get; set; }

    public IEnumerable<ProfileDto> Profiles { get; set; }
    protected IEnumerable<SkillDto> Skills { get; set; }
    
    public string firstName { get; set; }

    protected override async Task OnInitializedAsync()
    {

         //Profiles = await SearchService.GetResults();
         //Skills = await ProfileService.GetSkills();

    }
    
}

