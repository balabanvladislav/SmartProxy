using System.Text;

namespace UTM.Domain
{
    public class HttpClientUtility
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<HttpResponseMessage> SendJson(string json, string url, string method)
        {
            var httpMethod = new HttpMethod(method.ToUpper());
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = content
            };

            var task = await _httpClient.SendAsync(request);

            return task;
        }
    }
}
