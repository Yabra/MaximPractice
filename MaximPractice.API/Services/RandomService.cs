namespace MaximPractice.API.Services;

public class RandomService
{
    private readonly HttpClient _httpClient;
    private readonly Random _random;

    public RandomService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _random = new Random();
    }

    public async Task<int> GetRandomNumberAsync(int max)
    {
        try
        {
            var responce = await _httpClient.GetFromJsonAsync<int[]>(
               $"http://www.randomnumberapi.com/api/v1.0/random?max={max - 1}&count=1");

            return responce![0];
        }
        catch
        {
            return _random.Next(max);
        }
    }
}
