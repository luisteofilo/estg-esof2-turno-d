using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.DBLayer.Extensions;

public static class InterviewExtensions
{
    public static void UpdateInterviewState(this Interview interview, InterviewState newState )
    {
        interview.InterviewState = newState;
    }
    
    public static bool IsFinished(this Interview interview)
    {
        if (interview.InterviewState == InterviewState.Canceled || interview.InterviewState == InterviewState.Completed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}