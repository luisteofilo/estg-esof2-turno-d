using Common.Dtos.Job;
using Common.Dtos.Position;

namespace Frontend.Services.Contracts;

public interface IPositionService
{
    Task CreatePosition(Guid jobId, PositionCreateDTO dto);
    
    Task<IEnumerable<PositionResponseDTO>> GetPositions();
}