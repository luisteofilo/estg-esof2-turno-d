using System.Net.Http.Json;
using Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Interview
{
    public class InterviewListBase : ComponentBase
    {
        [Inject] protected IInterviewService InterviewService { get; set; }
        [Inject] protected ICandidateService CandidateService { get; set; }
        [Inject] protected IInterviewerService InterviewerService { get; set; }

        protected IEnumerable<InterviewDto> interviews;
        protected Dictionary<Guid, string> candidateNames = new Dictionary<Guid, string>();
        protected Dictionary<Guid, string> interviewerNames = new Dictionary<Guid, string>();

        protected DateTime currentDateTime;
        protected Dictionary<Guid, bool> presenceMarked = new Dictionary<Guid, bool>();
        protected Dictionary<Guid, bool> lightOn = new Dictionary<Guid, bool>();
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
                interviews = await InterviewService.GetInterviewsAsync();

                var candidates = await CandidateService.GetCandidatesAsync();
                var interviewers = await InterviewerService.GetInterviewersAsync();

                candidateNames = candidates.ToDictionary(c => c.CandidateId, c => c.Name);
                interviewerNames = interviewers.ToDictionary(i => i.InterviewerId, i => i.Name);

                foreach (var interview in interviews)
                {
                    bool isPresenceMarked = await InterviewService.GetPresenceMarkedAsync(interview.InterviewId);
                    presenceMarked[interview.InterviewId] = isPresenceMarked;
                    lightOn[interview.InterviewId] = isPresenceMarked;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error initializing data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        //Metodo para o botão Cancelar
        protected async Task CancelInterview(Guid interviewId)
        {
            try
            {
                await InterviewService.CancelInterviewAsync(interviewId);
                await LoadDataAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error canceling interview: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        //Metodo para passar os states para Strings
        protected string GetInterviewState(InterviewState state)
        {
            return state switch
            {
                InterviewState.Scheduled => "Scheduled",
                InterviewState.Missed => "Missed",
                InterviewState.OnGoing => "On Going",
                InterviewState.Completed => "Completed",
                InterviewState.Canceled => "Canceled",
                _ => "Unknown"
            };
        }

        //Metodo para atualizar a data e hora ao segundo
        private async Task StartPollingAsync()
        {
            while (_shouldContinuePolling)
            {
                await RefreshDate();
                StateHasChanged();
                await Task.Delay(1000); // Aguarde 1 seg.
            }
        }  
        
        
        //Metodo para o botão de Refresh da DataHora de hoje
        protected async Task RefreshDate()
        {
            try
            {
                var response = await InterviewService.GetCurrentDateTimeAsync();
                if (response != null)
                {
                    currentDateTime = response;
                    await UpdateInterviewsState();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching current date and time: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        //Metodo para atualizar o estado da entrevista consoante a data e Hora recebida do refreshDate
        protected async Task UpdateInterviewsState()
        {
            foreach (var interview in interviews)
            {
                if (currentDateTime >= interview.DateHourStart && currentDateTime <= interview.DateHourEnd && interview.InterviewState != InterviewState.Canceled)
                {
                    interview.InterviewState = InterviewState.OnGoing;
                    await InterviewService.UpdateInterviewStateAsync(interview.InterviewId, InterviewState.OnGoing);
                }
                else if (currentDateTime > interview.DateHourEnd)
                {
                    if (interview.InterviewState == InterviewState.OnGoing)
                    {
                        var newState = presenceMarked[interview.InterviewId] ? InterviewState.Completed : InterviewState.Missed;
                        interview.InterviewState = newState;
                        await InterviewService.UpdateInterviewStateAsync(interview.InterviewId, newState);
                    }
                }
            }
            StateHasChanged();
        }

        //O botão Marcar Presença
        protected async Task MarkPresence(Guid interviewId)
        {
            if (presenceMarked.ContainsKey(interviewId))
            {
                presenceMarked[interviewId] = true;
                lightOn[interviewId] = true;
                await InterviewService.MarkPresenceAsync(interviewId);
            }
            StateHasChanged();
        }
    }
}
