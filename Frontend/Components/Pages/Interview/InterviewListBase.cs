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

        private IEnumerable<InterviewDto> interviews;
        protected IEnumerable<InterviewDto> filteredInterviews;
        protected Dictionary<Guid, string> candidateNames = new Dictionary<Guid, string>();
        protected Dictionary<Guid, string> interviewerNames = new Dictionary<Guid, string>();

        protected DateTime currentDateTime;
        protected Dictionary<Guid, bool> presenceMarked = new Dictionary<Guid, bool>();
        protected Dictionary<Guid, bool> lightOn = new Dictionary<Guid, bool>();
        private bool _shouldContinuePolling = true;

        private string searchText = string.Empty;
        private string selectedState = "All";
        private string selectedCandidate = string.Empty;
        private string selectedInterviewer = string.Empty;
        private DateTime? startDateStart = null;
        private DateTime? endDateStart = null;
        private DateTime? startDateEnd = null;
        private DateTime? endDateEnd = null;
        protected string selectedLocation = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            _ = StartPollingAsync();
        }

        // Aqui Carregamos os dados para atualizar a lista
        protected async Task LoadDataAsync()
        {
            try
            {
                interviews = await InterviewService.GetInterviewsAsync();
                filteredInterviews = interviews;

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

                FilterInterviews();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error initializing data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        // Metodo para o botão Cancelar
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

        // Metodo para passar os states para Strings
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

        // Metodo para atualizar a data e hora ao segundo
        private async Task StartPollingAsync()
        {
            while (_shouldContinuePolling)
            {
                await RefreshDate();
                StateHasChanged();
                await Task.Delay(1000); // Aguarde 1 seg.
            }
        }

        // Metodo para o botão de Refresh da DataHora de hoje
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

        // Metodo para atualizar o estado da entrevista consoante a data e Hora recebida do refreshDate
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

        // O botão Marcar Presença
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

        protected void OnSearchTextChanged(ChangeEventArgs e)
        {
            searchText = e.Value.ToString();
            FilterInterviews();
        }

        protected void OnStateChanged(ChangeEventArgs e)
        {
            selectedState = e.Value.ToString();
            FilterInterviews();
        }

        protected void OnCandidateChanged(ChangeEventArgs e)
        {
            selectedCandidate = e.Value.ToString();
            FilterInterviews();
        }

        protected void OnInterviewerChanged(ChangeEventArgs e)
        {
            selectedInterviewer = e.Value.ToString();
            FilterInterviews();
        }

        protected void OnLocationChanged(ChangeEventArgs e)
        {
            selectedLocation = e.Value.ToString();
            FilterInterviews();
        }

        protected void OnStartDateStartChanged(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value.ToString(), out var date))
            {
                startDateStart = date;
            }
            else
            {
                startDateStart = null;
            }
            FilterInterviews();
        }

        protected void OnEndDateStartChanged(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value.ToString(), out var date))
            {
                endDateStart = date;
            }
            else
            {
                endDateStart = null;
            }
            FilterInterviews();
        }

        protected void OnStartDateEndChanged(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value.ToString(), out var date))
            {
                startDateEnd = date;
            }
            else
            {
                startDateEnd = null;
            }
            FilterInterviews();
        }

        protected void OnEndDateEndChanged(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value.ToString(), out var date))
            {
                endDateEnd = date;
            }
            else
            {
                endDateEnd = null;
            }
            FilterInterviews();
        }

        // Metodo para filtrar as entrevistas
        protected void FilterInterviews()
        {
            filteredInterviews = interviews.Where(i =>
                (string.IsNullOrEmpty(searchText) || candidateNames[i.CandidateId].Contains(searchText, StringComparison.OrdinalIgnoreCase)) &&
                (selectedState == "All" || GetInterviewState(i.InterviewState) == selectedState) &&
                (string.IsNullOrEmpty(selectedCandidate) || i.CandidateId.ToString() == selectedCandidate) &&
                (string.IsNullOrEmpty(selectedInterviewer) || i.InterviewerId.ToString() == selectedInterviewer) &&
                (!startDateStart.HasValue || i.DateHourStart >= startDateStart) &&
                (!endDateStart.HasValue || i.DateHourStart <= endDateStart) &&
                (!startDateEnd.HasValue || i.DateHourEnd >= startDateEnd) &&
                (!endDateEnd.HasValue || i.DateHourEnd <= endDateEnd) &&
                (string.IsNullOrEmpty(selectedLocation) || i.Location.Contains(selectedLocation, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }
}
