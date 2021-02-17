using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using static System.Net.HttpStatusCode;


using Minitwit.Entities;
using Repos;
using Controllers;

namespace DB.Tests
{
    public class FollowerControllerTest
    {
        [Fact]
        public void POST_given_follow_with_existing_usernames()
        {
            var followRepo = new Mock<IFollowerRepository>();
            var userRepo = new Mock<IUserRepository>();

            var controller = new FollowerController(followRepo.Object, userRepo.Object);

            // var actual = controller.Follow("user1");

            // Assert.Equal(NotFound.ToString(), actual.ToString());
        }
    }
}