using Common.Dtos.Interview;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Components.Pages.Interview
{
    public class InterviewCreationBase : ComponentBase
    {
        [Inject] protected IInterviewService InterviewService { get; set; }
        [Inject] protected ICandidateService CandidateService { get; set; }
        [Inject] protected IInterviewerService InterviewerService { get; set; }

        protected InterviewDto Interview { get; set; } = new InterviewDto();
        protected CandidateDto NewCandidate { get; set; } = new CandidateDto();
        protected InterviewerDto NewInterviewer { get; set; } = new InterviewerDto();
        protected IEnumerable<CandidateDto> Candidates { get; set; }
        protected IEnumerable<InterviewerDto> Interviewers { get; set; }

        protected List<CandidateDto> AvailableCandidates { get; set; } = new List<CandidateDto>();
        protected List<InterviewerDto> AvailableInterviewers { get; set; } = new List<InterviewerDto>();

        protected string ErrorMessage { get; set; } = string.Empty;
        protected bool ShowError { get; set; } = false;
        protected bool IsInterviewCreated { get; set; } = false;
        protected bool IsCandidateCreated { get; set; } = false;
        protected bool IsInterviewerCreated { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            // Isto aqui é o tempo padrão da entrevista, é apenas uma recomendação no fundo
            Interview.DateHourStart = DateTime.Now;
            Interview.DateHourEnd = DateTime.Now.AddMinutes(30); 

            await LoadDataAsync();
        }

        protected async Task LoadDataAsync()
        {
            try
            {
                Candidates = await CandidateService.GetCandidatesAsync();
                Interviewers = await InterviewerService.GetInterviewersAsync();

                if (Candidates != null)
                {
                    AvailableCandidates = Candidates.ToList();
                }

                if (Interviewers != null)
                {
                    AvailableInterviewers = Interviewers.ToList();
                }
            }
            catch (HttpRequestException ex)
            {
                ShowError = true;
                ErrorMessage = $"Error initializing data: {ex.Message}";
            }
        }

        protected async Task HandleSubmit()
        {
            ShowError = false;

            try
            {
                // Validar se a dataHora do início é antes do que a dataHora do fim
                if (Interview.DateHourStart >= Interview.DateHourEnd)
                {
                    ShowError = true;
                    ErrorMessage = "The start time must be before the end time.";
                    return;
                }

                // Validar a duração da entrevista (entre 30 minutos e 1 hora e 30 minutos)
                TimeSpan duration = Interview.DateHourEnd - Interview.DateHourStart;
                if (duration < TimeSpan.FromMinutes(30) || duration > TimeSpan.FromMinutes(90))
                {
                    ShowError = true;
                    ErrorMessage = "The interview duration must be between 30 minutes and 1 hour and 30 minutes.";
                    return;
                }

                // Criar a entrevista
                await InterviewService.CreateInterviewAsync(Interview);
                IsInterviewCreated = true;
                StateHasChanged();
            }
            catch (HttpRequestException ex)
            {
                ShowError = true;
                ErrorMessage = $"Error creating interview: {ex.Message}";
            }
        }

        protected async Task CreateCandidate()
        {
            ShowError = false;
            try
            {
                // Criar o Candidato
                await CandidateService.CreateCandidateAsync(NewCandidate);
                await LoadDataAsync();
                IsCandidateCreated = true;
            }
            catch (HttpRequestException ex)
            {
                ShowError = true;
                ErrorMessage = ex.Message;
            }
        }

        protected async Task CreateInterviewer()
        {
            ShowError = false;
            try
            {
                // Criar o Entrevistador
                await InterviewerService.CreateInterviewerAsync(NewInterviewer);
                await LoadDataAsync();
                IsInterviewerCreated = true;
            }
            catch (HttpRequestException ex)
            {
                ShowError = true;
                ErrorMessage = ex.Message;
            }
        }

        //Este metodo foi criado apenas com o proposito de atualizar
        //o Current Date Time na pagina de Listar Entrevistas apos Refresh
        protected string GetCurrentDateTimeString()
        {
            return DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
        }
    }
}
