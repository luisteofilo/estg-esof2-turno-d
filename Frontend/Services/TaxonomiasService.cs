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
    }
}