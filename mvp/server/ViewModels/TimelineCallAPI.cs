using System.Collections.Generic;
using System.Threading.Tasks;
using Minitwit.Entities;
using mvp.Extensions;
using System.Net.Http;
using System.Text.Json;

namespace mvp.ViewModels
{
    public class TimelineCallAPI : ITimelineCallAPI
    {
        private readonly IUserState _userState;
        private readonly HttpClient _httpClient;
        private readonly IUtilViewModel _utils;

        public TimelineCallAPI(IUserState userState, IUtilViewModel utils, HttpClient httpClient)
        {
            _userState = userState;
            _utils = utils;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserMessageDTO>> Timeline()
        {
            if (_userState.User != null && _userState.User.user_id >= 0) 
            {
                var content = await _httpClient.GetToAPIAsync(_utils.APIURL + "msgs/" + _userState.User.username + "/follows");

                _utils.UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
                (
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return _utils.UserMessageDTO;
            }
            else
            {
                return new List<UserMessageDTO>();
            }
        }

        public async Task<IEnumerable<UserMessageDTO>> PublicTimeline()
        {
            var content = await _httpClient.GetToAPIAsync(_utils.APIURL + "msgs/");

            _utils.UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return _utils.UserMessageDTO;
        }

        public async Task<IEnumerable<UserMessageDTO>> UserTimeline(string username)
        {
            var content = await _httpClient.GetToAPIAsync(_utils.APIURL + "msgs/" + username);

            _utils.UserMessageDTO = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserMessageDTO>>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return _utils.UserMessageDTO;
        }
    }
}