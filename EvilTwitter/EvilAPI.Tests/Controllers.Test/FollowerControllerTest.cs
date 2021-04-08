using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using static System.Net.HttpStatusCode;


using Minitwit.Entities;
using EvilAPI.Repos;
using EvilAPI.Controllers;

namespace EvilAPI.Tests
{
    public class FollowerControllerTest
    {
        [Fact]
        public void POST_given_follow_with_existing_usernames()
        {
            var followRepo = new Mock<IFollowerRepository>();
            var userRepo = new Mock<IUserRepository>();
            var latest = new Mock<latest_global>();

            var controller = new FollowerController(followRepo.Object, userRepo.Object, latest.Object);

            // var actual = controller.Follow("user1");

            // Assert.Equal(NotFound.ToString(), actual.ToString());
        }
    }
}