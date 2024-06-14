using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESOF.WebApp.WebAPI.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        public async Task<List<Interview>> GetAllInterviews()
        {
            var db = new ApplicationDbContext();

            var interviews = await db.Interviews.ToListAsync();

            return interviews.Select(i => new Interview
            {
                InterviewId = i.InterviewId,
                InterviewState = i.InterviewState,
                Date = i.Date,
                Hour = i.Hour,
                Location = i.Location,
            }).ToList();
        }
    }
}