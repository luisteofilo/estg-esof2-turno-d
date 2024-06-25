namespace Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities.Interviews;
public static class DtoConversion
{
    //ConversãoDto para as Interviews
    public static IEnumerable<InterviewDto> InterviewsConvertToDto(this IEnumerable<Interview> interviews)
    {
        return interviews.Select(interview => interview.InterviewConvertToDto()).ToList();
    }

    public static InterviewDto CopyInterviewDto(this InterviewDto interviewDto)
    {
        return new InterviewDto
        {
            InterviewId = interviewDto.InterviewId,
            InterviewState = interviewDto.InterviewState,
            Location = interviewDto.Location,
            DateHourStart = interviewDto.DateHourStart,
            DateHourEnd = interviewDto.DateHourEnd,
            PresenceMarked = interviewDto.PresenceMarked,
            CandidateId = interviewDto.CandidateId,
            InterviewerId = interviewDto.InterviewerId,
        };
    }

    public static InterviewDto InterviewConvertToDto(this Interview interview)
    {
        return new InterviewDto
        {
            InterviewId = interview.InterviewId,
            InterviewState = interview.InterviewState,
            Location = interview.Location,
            DateHourStart = interview.DateHourStart,
            DateHourEnd = interview.DateHourEnd,
            PresenceMarked = interview.PresenceMarked,
            CandidateId = interview.CandidateId,
            InterviewerId = interview.InterviewerId
        };
    }

    public static Interview DtoConvertToInterview(this InterviewDto interviewDto)
    {
        return new Interview
        {
            InterviewId = interviewDto.InterviewId,
            InterviewState = interviewDto.InterviewState,
            Location = interviewDto.Location,
            DateHourStart = interviewDto.DateHourStart,
            DateHourEnd = interviewDto.DateHourEnd,
            PresenceMarked = interviewDto.PresenceMarked,
            CandidateId = interviewDto.CandidateId,
            InterviewerId = interviewDto.InterviewerId,
        };
    }
    
    
    //ConversãoDto para os Candidates
    public static CandidateDto CandidateConvertToDto(this Candidate candidate)
    {
        return new CandidateDto
        {
            CandidateId = candidate.CandidateId,
            Name = candidate.Name
        };
    }

    public static Candidate DtoConvertToCandidate(this CandidateDto candidateDto)
    {
        return new Candidate
        {
            CandidateId = candidateDto.CandidateId,
            Name = candidateDto.Name
        };
    }

    //ConversãoDto para os Interviewers
    public static InterviewerDto InterviewerConvertToDto(this Interviewer interviewer)
    {
        return new InterviewerDto
        {
            InterviewerId = interviewer.InterviewerId,
            Name = interviewer.Name
        };
    }

    public static Interviewer DtoConvertToInterviewer(this InterviewerDto interviewerDto)
    {
        return new Interviewer
        {
            InterviewerId = interviewerDto.InterviewerId,
            Name = interviewerDto.Name
        };
    }
}
