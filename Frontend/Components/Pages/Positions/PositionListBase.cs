using Common.Dtos.Interview;
using Common.Dtos.Position;
using Frontend.Services;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Positions;

public class PositionListBase : ComponentBase
    {
        [Inject] protected IPositionService PositionService { get; set; }
        
        protected IEnumerable<PositionResponseDTO> positions;
        
        private bool _shouldContinuePolling = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            _ = StartPollingAsync();
        }

        //Aqui Carregamos os dados para atualizar a lista
        protected async Task LoadDataAsync()
        {
            try
            {
                positions = await PositionService.GetPositions();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error initializing data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        
        private async Task StartPollingAsync()
        {
            while (_shouldContinuePolling)
            {
                StateHasChanged();
                await Task.Delay(1000); // Wait 1 sec
            }
        }  
}