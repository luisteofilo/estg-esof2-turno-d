
namespace Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities;

public static class DtoConversion
{
    // Métodos para InterviewDto
    public static IEnumerable<InterviewDto> InterviewsConvertToDto(this IEnumerable<Interview> Interviews)
    {
        return Interviews.Select(Interview => Interview.InterviewConvertToDto()).ToList();
    }

    public static InterviewDto CopyInterviewDto(this InterviewDto InterviewDto)
    {
        return new InterviewDto
        {
            InterviewId = InterviewDto.InterviewId,
            CandidateId = InterviewDto.CandidateId,
            InterviewerId = InterviewDto.InterviewerId,
            SlotId = InterviewDto.SlotId,
            InterviewState = InterviewDto.InterviewState,
            Location = InterviewDto.Location,
            DateHourStart = InterviewDto.DateHourStart,
            DateHourEnd = InterviewDto.DateHourEnd
        };
    }

    public static InterviewDto InterviewConvertToDto(this Interview Interview)
    {
        return new InterviewDto
        {
            InterviewId = Interview.InterviewId,
            CandidateId = Interview.CandidateId,
            InterviewerId = Interview.InterviewerId,
            SlotId = Interview.SlotId,
            InterviewState = Interview.InterviewState,
            Location = Interview.Location,
            DateHourStart = Interview.DateHourStart,
            DateHourEnd = Interview.DateHourEnd
        };
    }

    public static Interview DtoConvertToInterview(this InterviewDto InterviewDto)
    {
        return new Interview
        {
            InterviewId = InterviewDto.InterviewId,
            CandidateId = InterviewDto.CandidateId,
            InterviewerId = InterviewDto.InterviewerId,
            SlotId = InterviewDto.SlotId,
            InterviewState = InterviewDto.InterviewState,
            Location = InterviewDto.Location,
            DateHourStart = InterviewDto.DateHourStart,
            DateHourEnd = InterviewDto.DateHourEnd
        };
    }

    // Métodos para CandidateDto
    public static IEnumerable<CandidateDto> CandidatesConvertToDto(this IEnumerable<Candidate> candidates)
    {
        return candidates.Select(candidate => candidate.CandidateConvertToDto()).ToList();
    }

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
    
    // Métodos para Interviewer
    public static IEnumerable<InterviewerDto> InterviewersConvertToDto(this IEnumerable<Interviewer> interviewers)
    {
        return interviewers.Select(interviewer => interviewer.InterviewerConvertToDto()).ToList();
    }

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

    // Métodos para SlotDto
    public static IEnumerable<SlotDto> SlotsConvertToDto(this IEnumerable<Slot> slots)
    {
        return slots.Select(slot => slot.SlotConvertToDto()).ToList();
    }

    public static SlotDto SlotConvertToDto(this Slot slot)
    {
        return new SlotDto
        {
            SlotId = slot.SlotId,
            InterviewId = slot.InterviewId,
            InterviewerId = slot.InterviewerId,
            Ocupied = slot.Ocupied,
            DateHourStart = slot.DateHourStart,
            DateHourEnd = slot.DateHourEnd
        };
    }

    public static Slot DtoToSlot(this SlotDto slotDto)
    {
        return new Slot
        {
            SlotId = slotDto.SlotId,
            InterviewId = slotDto.InterviewId,
            InterviewerId = slotDto.InterviewerId,
            Ocupied = slotDto.Ocupied,
            DateHourStart = slotDto.DateHourStart,
            DateHourEnd = slotDto.DateHourEnd
        };
    }
}