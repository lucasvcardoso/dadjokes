using System.Net.Http;
using System.Threading.Tasks;

namespace IHazDadJokes.Infrastructure.HttpLib
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> Get(string url);
    }
}