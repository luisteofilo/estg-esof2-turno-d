namespace ESOF.WebApp.Scraper.Helpers;

public static class HttpClientHelper
{
    private static readonly HttpClient _httpClient = new HttpClient();

    static HttpClientHelper()
    {
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
    }

    public static async Task<string> GetHtmlAsync(string url)
    {
        try
        {
            return await _httpClient.GetStringAsync(url);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}