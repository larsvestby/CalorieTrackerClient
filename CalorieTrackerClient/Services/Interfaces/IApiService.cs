using System.Net.Http;

namespace CalorieTrackerClient.Services.Interfaces
{
    public interface IApiService
    {
        Task<HttpResponseMessage> SendJsonAsync<T>(
            HttpMethod method,
            string url,
            T payload
        );

        Task<TResponse?> ReadJsonAsync<TResponse>(HttpResponseMessage response);

        Task<string> ReadRawAsync(HttpResponseMessage response);
    }
}