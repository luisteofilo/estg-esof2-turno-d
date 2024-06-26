using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common.Dtos.Taxonomias;

namespace Frontend.Services
{
    public class TaxonomiasService : ITaxonomiasService
    {
        private readonly HttpClient _httpClient;

        public TaxonomiasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VerticalDto>> GetTaxonomiasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VerticalDto>>("api/taxonomias");
        }

        public async Task CreateVerticalAsync(VerticalDto newVertical)
        {
            var response = await _httpClient.PostAsJsonAsync("api/taxonomias", newVertical);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task<List<VerticalDto>> GetTaxonomiasUserAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<List<VerticalDto>>($"api/taxonomias/user/{id}");
        }
        
        public async Task UpdateVerticalAsync(VerticalDto updatedVertical)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/taxonomias/{updatedVertical.VerticalId}", updatedVertical);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeleteVerticalAsync(Guid verticalId)
        {
            var response = await _httpClient.DeleteAsync($"api/taxonomias/{verticalId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                // Handle deletion failure gracefully
                return false;
            }
        }
    }
}
