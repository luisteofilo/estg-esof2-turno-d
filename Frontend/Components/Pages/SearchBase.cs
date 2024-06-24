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
        
        protected IEnumerable<string> Locations { get; set; }
        
        [SupplyParameterFromQuery(Name= "location")]
        public string location { get; set; }
        
        [SupplyParameterFromQuery(Name= "skill")]
        public string Skill { get; set; }
        
        
        

        protected override async Task OnInitializedAsync()
        {
            var searchMethods = new Dictionary<Func<bool>, Func<Task>>()
            {
                { () => !string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(Skill) && string.IsNullOrEmpty(location), 
                    async () => Profiles = await SearchService.GetResults(firstName) },

                { () => !string.IsNullOrEmpty(Skill) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(location), 
                    async () => Profiles = await SearchService.GetResultsBySkill(Skill) },

                { () => !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(Skill) && string.IsNullOrEmpty(location), 
                    async () => Profiles = await SearchService.GetResultsBySkill_Name(Skill, firstName) },

                { () => !string.IsNullOrEmpty(location) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(Skill), 
                    async () => Profiles = await SearchService.GetResultByLocation(location) },

                { () => !string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(Skill), 
                    async () => Profiles = await SearchService.GetResultsBySkill_Name_Location( Skill, firstName, location) },

               { () => !string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(firstName), 
                    async () => Profiles = await SearchService.GetResultsByLocation_Name(location, firstName) },

                { () => !string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(Skill), 
                    async () => Profiles = await SearchService.GetResultsByLocation_Skill(location, Skill) }
            };

            foreach (var method in searchMethods)
            {
                if (method.Key())
                {
                    SearchService.searchPerformed = true;
                    await method.Value();
                    break;
                }
            }

            
            Skills = await ProfileService.GetSkills();
            Locations = await SearchService.GetLocations();

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