using Xunit;
using mvp.ViewModels;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace EvilClient.Tests
{
    public class UtilViewModelTests
    {
        private readonly HttpClient httpClient;
        private IUtilViewModel util;

        public UtilViewModelTests()
        {
            httpClient = new HttpClient();
            // navigationManager.Setup(nm => nm.BaseUri).Returns("");

            util = new UtilViewModel(httpClient);
        }

        [Fact]
        public void Given_password_hashed_MD5_hasher_return_the_same()
        {
            //Given
            var passwordHashedMD5 = "5f4dcc3b5aa765d61d8327deb882cf99";
        
            //When
            var actual = util.MD5Hasher("password");
        
            //Then
            Assert.Equal(passwordHashedMD5, actual);
        }
    }
}
