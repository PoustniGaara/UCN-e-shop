using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiClient
{
    public static class RestClientExtensions
    {
        public static async Task<RestResponse<T>> RequestAsync<T>(this RestClient client, Method method, string resource = null, object body = null)
        {
            var request = new RestRequest(resource, method);
            if (body != null)
            {
                request.AddJsonBody(JsonSerializer.Serialize(body));
            }
            return await client.ExecuteAsync<T>(request, method);
        }

        public static async Task<RestResponse> RequestAsync(this RestClient client, Method method, string resource = null, object body = null)
        {
            var request = new RestRequest(resource, method);
            if (body != null)
            {
                request.AddJsonBody(JsonSerializer.Serialize(body));
            }
            return await client.ExecuteAsync(request, method);
        }
    }
}
