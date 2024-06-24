
using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Interview;
public class CandidateDto
{
    public Guid CandidateId { get; set; }
    public string Name { get; set; }
}