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
    public class AuthenticationCallApiTests
    {
        private Mock<HttpMessageHandler> handlerMock;
        private readonly HttpClient _httpClient;
        private IUserState _userState;
        private IUtilViewModel _util;
        private IAuthenticationCallApi _auth;

        public AuthenticationCallApiTests()
        {
            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };

            _userState = new UserState();
            _util = new UtilViewModel(_httpClient);

            _auth = new AuthenticationCallApi(_util, _userState, _httpClient);
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
        public async Task Given_url_that_returns_200_SignIn_returns_true_and_update_user_state()
        {
            //Given
            var userId = 1;
            var username = "test";
            var email = "mail@test.com";
            var pwd = "123456";

            var userJson = "{\"user_id\":" + userId + ",\"username\":\"" + username + "\",\"email\":\"" + email + "\",\"pwd\":\"" + pwd + "\"}";
            setUpHandlerMock(HttpStatusCode.OK, userJson);

            //When
            var actual = await _auth.SignIn("AnyUser");
            var actualUser = _userState.User;

            //Then
            Assert.Equal(true, actual);
            Assert.Equal(userId, actualUser.user_id);
            Assert.Equal(username, actualUser.username);
            Assert.Equal(email, actualUser.email);
            Assert.Equal(pwd, actualUser.pwd);
        }

        [Fact]
        public async Task Given_url_that_returns_404_SignIn_returns_false_and_does_not_update_user_state()
        {
            //Given
            var userJson = "{}";
            setUpHandlerMock(HttpStatusCode.NotFound, userJson);

            //When
            var actual = await _auth.SignIn("AnyUser");

            //Then
            Assert.Equal(false, actual);
            Assert.Equal(-1, _userState.User.user_id);
        }

        [Fact]
        public async Task Given_incorrect_user_info_SignUp_returns_BadRequest()
        {
            //Given
            setUpHandlerMock(HttpStatusCode.BadRequest, "");

            //When
            var actual = await _auth.SignUp("", "", "");

            //Then
            Assert.Equal(HttpStatusCode.BadRequest, actual);
        }
        
        [Fact]
        public async Task Given_correct_user_info_SignUp_returns_Ok()
        {
            //Given
            setUpHandlerMock(HttpStatusCode.NoContent, "");

            //When
            var actual = await _auth.SignUp("", "", "");

            //Then
            Assert.Equal(HttpStatusCode.OK, actual);
        }
    }
}
