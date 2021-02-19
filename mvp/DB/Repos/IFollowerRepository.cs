using System.Collections.Generic;
using System.Net;
using Minitwit.Entities;

namespace Repos
{
    public interface IFollowerRepository
    {
        HttpStatusCode FollowUser(string usernameInSession, string usernameToFollow);

        HttpStatusCode UnfollowUser(string usernameInSession, string usernameToUnfollow);

        public bool DoesUserFollow(int user_id, int isFollowed_id);
    }
}