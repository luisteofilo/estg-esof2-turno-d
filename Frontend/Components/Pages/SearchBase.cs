using Frontend.Services;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;
namespace Frontend.Components.Pages;

public class SearchBase: ComponentBase
{
    [Inject]
    public ISearchService SearchService { get; set; }
    
    public ProfileDto Profile { get; set; }
    
     protected override async Task OnInitializedAsync()
    {
        Profile = await SearchService.GetResults();

    }
}