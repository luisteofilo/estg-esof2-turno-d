using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages;

public class ProfileBase : ComponentBase
{
    [Inject]
    public IProfileService ProfileService { get; set; }
    
    public ProfileDto Profile { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Profile = await ProfileService.GetProfile();

    }
}