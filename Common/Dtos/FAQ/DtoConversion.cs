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

        return q;
    }
    
    public static AnswerDto AnswerConvertToDto(Answer answer)
    {
        AnswerDto a = new AnswerDto();
        a.AnswerId = answer.AnswerId;
        a.AnswerText = answer.AnswerText;
        a.AuthorEmail = answer.Author.Email;

        return a;
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