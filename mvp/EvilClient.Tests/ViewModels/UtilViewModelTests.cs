using Xunit;
using mvp.ViewModels;
using System.Net.Http;
using System.Security.Cryptography;

namespace EvilClient.Tests
{
    public class UtilViewModelTests
    {
        private readonly HttpClient httpClient;
        private IUtilViewModel util;
        private readonly string password = "password";

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
            var actualMD5Hashed = util.stringToHash(password, MD5.Create());
        
            //Then
            Assert.Equal(passwordHashedMD5, actualMD5Hashed);
        }
    }
}
