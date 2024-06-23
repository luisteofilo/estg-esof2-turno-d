using Common.Dtos.Profile;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (!string.IsNullOrEmpty(firstName))
            {
                Profiles = await SearchService.GetResults(firstName);
            }
            if (!string.IsNullOrEmpty(Skill))
            {
                ProfilesSkill = await SearchService.GetResultsBySkill(Skill);
            }
            Skills = await ProfileService.GetSkills();
        }
    }
}