using System.Text;
using Newtonsoft.Json;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling the API: " + $"{response.ReasonPhrase}");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content)!;
        }
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url, content);
        }
        public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient httpClient, string requestUri, object value)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
            return await httpClient.PutAsync(requestUri, content);
        }
    }
    
}
