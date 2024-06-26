using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Taxonomias;
using System;

namespace Frontend.Services
{
    public interface ITaxonomiasService
    {
        Task<List<VerticalDto>> GetTaxonomiasAsync();
        Task CreateVerticalAsync(VerticalDto newVertical);
        Task<List<VerticalDto>> GetTaxonomiasUserAsync(Guid id);
        Task UpdateVerticalAsync(VerticalDto updatedVertical);
        Task<bool> DeleteVerticalAsync(Guid verticalId);
    }
}