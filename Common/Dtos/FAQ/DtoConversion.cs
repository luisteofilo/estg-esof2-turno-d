using ESOF.WebApp.DBLayer.Entities.FAQ;

namespace Common.Dtos.FAQ;

public static class DtoConversion
{
    public static QuestionDto QuestionConvertToDto(Question question)
    {
        QuestionDto q = new QuestionDto();
        q.QuestionId = question.QuestionId;
        q.QuestionText = question.QuestionText;
        q.VerifiedAnswer = question.VerifiedAnswer?.AnswerText;
        q.Verifier = question.Verifier?.UserId ?? Guid.Empty;
        if (question.Answers == null)
        {
            q.Answers = new List<AnswerDto>();
        }
        else
        {
            q.Answers = AnswerConvertToDto(question.Answers);
        }
        
        return q;
    }
    
    public static AnswerDto AnswerConvertToDto(Answer answer)
    {
        AnswerDto a = new AnswerDto();
        a.AnswerId = answer.AnswerId;
        a.AnswerText = answer.AnswerText;
        a.AuthorEmail = answer.AuthorEmail;
        a.QuestionId = answer.Question?.QuestionId;
        a.CreatedAt = answer.CreatedAt.ToString("yyyy-MM-dd HH:mm");

        return a;
    }

    public static JobDto JobConvertToDto(Job job)
    {
        JobDto j = new JobDto();
        j.JobId = job.JobId;
        j.JobTitle = job.JobTitle;
        return j;
    }
    
    public static IEnumerable<JobDto> JobConvertToDto(IEnumerable<Job> jobs)
    {
        return jobs.Select(JobConvertToDto);
    }

    public static IEnumerable<QuestionDto> QuestionConvertToDto(IEnumerable<Question> questions)
    {
        return questions.Select(QuestionConvertToDto);
    }
    
    public static IEnumerable<AnswerDto> AnswerConvertToDto(IEnumerable<Answer> answers)
    {
        return answers.Select(AnswerConvertToDto);
    }
}