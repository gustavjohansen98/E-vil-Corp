namespace EvilApp.ViewModels
{
    public interface IFollowCallAPI
    {
        Task<bool> IsFollowed(string username1, string username2);

        Task FollowUser(string user, string userToUnfollow);

        Task UnfollowUser(string user, string userToFollow);
    }
}