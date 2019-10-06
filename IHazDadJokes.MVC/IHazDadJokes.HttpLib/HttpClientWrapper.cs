using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IHazDadJokes.Infrastructure.HttpLib
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public async Task<HttpResponseMessage> Get (string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.GetAsync(url);
            }
        }
    }
}
