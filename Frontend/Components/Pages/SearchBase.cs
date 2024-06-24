using Common.Dtos.Profile;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.Components.Layout;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Frontend.Components.Pages
{
    public class SearchBase : ComponentBase
    {
        [Inject] public ISearchService SearchService { get; set; }
        [Inject] protected IProfileService ProfileService { get; set; }
        
        [SupplyParameterFromQuery(Name= "searchText")]
        public string firstName { get; set; } 

        protected IEnumerable<ProfileDto> Profiles { get; set; }
        protected IEnumerable<ProfileDto> ProfilesSkill { get; set; }
        protected IEnumerable<SkillDto> Skills { get; set; }
        
        
        [SupplyParameterFromQuery(Name= "skill")]
        public string Skill { get; set; }

        

        protected override async Task OnInitializedAsync()
        {
            
            if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(Skill))
            {
                SearchService.searchPerformed = true;
                Profiles = await SearchService.GetResults(firstName);
            }
            if (!string.IsNullOrEmpty(Skill) && string.IsNullOrEmpty(firstName))
            {
                SearchService.searchPerformed = true;
                Profiles = await SearchService.GetResultsBySkill(Skill);
            }

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(Skill))
            {
                SearchService.searchPerformed = true;
                Profiles = await SearchService.GetResultsBySkill_Name(Skill, firstName);
            }
            Skills = await ProfileService.GetSkills();
            
        }
        
        public async ValueTask DisposeAsync()
        {
            firstName = string.Empty;
            Skill = string.Empty;
            SearchService.searchPerformed = false;
            
            await Task.CompletedTask;
        }
        
        
    }
}