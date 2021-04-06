using Xunit;
using Moq;
using EvilClient.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;
using System.Net;
using System;

namespace EvilClient.Tests
{
    public class MessageCallApiTests
    {
        private Mock<HttpMessageHandler> handlerMock;
        private readonly HttpClient _httpClient;
        private IUserState _userState;
        private IUtilViewModel _util;
        private IMessageCallApi _messageCallApi;

        public MessageCallApiTests()
        {
            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };

            _userState = new UserState();
            _util = new UtilViewModel(_httpClient);

            _messageCallApi = new MessageCallApi(_httpClient, _util, _userState);
        }

        void setUpHandlerMock(HttpStatusCode returnCode, string returnMessage)
        {
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = returnCode,
                    Content = new StringContent(returnMessage),
                })
                .Verifiable();
        }

        [Fact]
        public async Task Given_correct_post_of_message_return_HttpStatusCode_of_200()
        {
            //Given
            setUpHandlerMock(HttpStatusCode.OK, "");

            //When
            var actual = await _messageCallApi.PostMessageToApi("This is the message");

            //Then
            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async Task Given_incorrect_post_of_message_return_HttpStatusCode_of_500()
        {
            //Given
            setUpHandlerMock(HttpStatusCode.InternalServerError, "");

            //When
            var actual = await _messageCallApi.PostMessageToApi("This is the message");

            //Then
            Assert.Equal(HttpStatusCode.InternalServerError, actual);
        }
    }
}
