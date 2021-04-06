using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EvilClient.ViewModels
{

    public class MessageCallApi : IMessageCallApi
    {
    private HttpClient _httpClient;
    private readonly IUtilViewModel _util;
    private readonly IUserState _userState;

    public MessageCallApi(HttpClient httpClient, IUtilViewModel utilViewModel, IUserState userState)
    {
        _httpClient = httpClient;
        _util = utilViewModel;
        _userState = userState;
    }

        public async Task<HttpStatusCode> PostMessageToApi(string text)
        {
            var messageObj = new
            {
                content = text
            };

            var json = JsonConvert.SerializeObject(messageObj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_util.APIURL + "msgs/" + _userState.User.username, data);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return response.StatusCode;
            }
            else
            {
                return response.StatusCode;
            }
        }
    }
}



