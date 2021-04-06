using System.Net;
using System.Threading.Tasks;

namespace EvilClient.ViewModels
{
    public interface IMessageCallApi
    {
        Task<HttpStatusCode> PostMessageToApi(string text);
    }
}