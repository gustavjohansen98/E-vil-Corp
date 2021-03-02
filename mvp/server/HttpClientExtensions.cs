using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mvp.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<string> GetToAPIAsync(this HttpClient httpClient, string URI)
        {
            var response = await httpClient.GetAsync(URI);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<HttpStatusCode> PostToAPIAsync(this HttpClient httpClient, string URI, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(URI, data);

            return response.StatusCode;
        }
    }
}
