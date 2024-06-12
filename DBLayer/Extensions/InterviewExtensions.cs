using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.DBLayer.Extensions;

public static class InterviewExtensions
{
    public static void UpdateInterviewState(this Interview interview, InterviewState newState )
    {
        interview.InterviewState = newState;
    }
    
    public static bool isOnGoing(this Interview interview)
    {
        if (interview.InterviewState == InterviewState.OnGoing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void setDateTimeOff(this Interview interview)
    {
        if (interview.isOnGoing())
        {
            /*Função para desativar o campo onde se insere o DateTime
             A entrevista OnGoing (a decorrer) não precisa de ter data e hora
             por isso quando a entrevista está a decorrer o campo de inserção
             data e hora não pode estar disponivel*/
        }
    }
}