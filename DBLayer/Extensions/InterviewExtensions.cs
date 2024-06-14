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
}