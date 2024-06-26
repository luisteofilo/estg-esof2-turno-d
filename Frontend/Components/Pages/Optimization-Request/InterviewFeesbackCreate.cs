using Common.Dtos.Interview;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using Frontend.Services;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Optimization_Request;

public class InterviewFeesbackCreate : ComponentBase
{
    [Inject] protected IInterviewFeedbackService InterviewFeedbackService { get; set; }

    [Inject] protected IInterviewService InterviewService { get; set; } 
    [Inject] protected ICandidateService CandidateService { get; set; } 
    [Inject] protected IInterviewerService InterviewerService { get; set; }
    [Inject] protected IJobService JobService { get; set; }

    protected InterviewFeedbackDTO InterviewFeedbackDto { get; set; }
    protected Guid InterviewFeedbackId { get; set; } 
    protected String Feedback { get; set; } = "";
    protected String RejectionReason { get; set; } = "";
    protected String OptimizationSuggestions { get; set; } = "";
    protected InterviewDto NewInterview { get; set; } = new InterviewDto();
    protected CandidateDto NewCandidate { get; set; } = new CandidateDto();
    protected InterviewerDto NewInterviewer { get; set; } = new InterviewerDto();
    protected JobDto NewJobDto { get; set; } = new JobDto();

    protected IEnumerable<CandidateDto> Candidates { get; set; }
    protected IEnumerable<InterviewerDto> Interviewers { get; set; }
    protected IEnumerable<InterviewDto> Interviews { get; set; }
    protected IEnumerable<JobDto> Jobs { get; set; }

    protected string ErrorMessage { get; set; } = string.Empty;
    protected bool ShowError { get; set; } = false;
    
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            ShowError = true;
            ErrorMessage = $"Error initializing data: {ex.Message}";
        }
    }
    
    
    
    protected async Task LoadDataAsync()
    {
        try
        {
            Candidates = await CandidateService.GetCandidatesAsync();
            Interviewers = await InterviewerService.GetInterviewersAsync();
            Jobs = await JobService.GetJobs();
            Interviews = await InterviewService.GetInterviewsAsync();
        }
        catch (HttpRequestException ex)
        {
            ShowError = true;
            ErrorMessage = $"Error initializing data: {ex.Message}";

            if (ex.InnerException != null)
            {
                ErrorMessage += $"\nInner Exception: {ex.InnerException.Message}";
            }
        }
        catch (Exception ex)
        {
            ShowError = true;
            ErrorMessage = $"An unexpected error occurred: {ex.Message}";
        }
    }

    
    protected async Task CreateInterviewFeedback()
    {
        ShowError = false;
        try
        {
            if (InterviewFeedbackDto == null)
                InterviewFeedbackDto = new InterviewFeedbackDTO();

            InterviewFeedbackDto.InterviewFeedbackId = InterviewFeedbackId;
            InterviewFeedbackDto.Candidate = NewCandidate?.CandidateId ?? Guid.Empty; 
            InterviewFeedbackDto.Interview = NewInterview?.InterviewId ?? Guid.Empty; 
            InterviewFeedbackDto.Interviewer = NewInterviewer?.InterviewerId ?? Guid.Empty; 
            InterviewFeedbackDto.Feedback = Feedback;
            InterviewFeedbackDto.RejectionReason = RejectionReason;
            InterviewFeedbackDto.OptimizationSuggestions = OptimizationSuggestions;
            InterviewFeedbackDto.JobId = NewJobDto?.JobId ?? Guid.Empty; 

            await InterviewFeedbackService.AddInterviewFeedbackAsync(InterviewFeedbackDto);
        }
        catch (HttpRequestException ex)
        {
            ShowError = true;
            ErrorMessage = ex.Message;
        }
    }

    
    
}