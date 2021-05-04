using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Minitwit.Entities;
using static System.Net.HttpStatusCode;

namespace EvilAPI.Repos
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly IMinitwitContext _context;
        private IUserRepository userRepo;

        public FollowerRepository(IMinitwitContext context)
        {
            _context = context;
            userRepo = new UserRepository(context);
        }

        public HttpStatusCode FollowUser(string usernameInSession, string usernameToFollow)
        {
            var userInSessionID = userRepo.GetUserIDFromUsername(usernameInSession);
            var userToFollowID = userRepo.GetUserIDFromUsername(usernameToFollow);

            if (userInSessionID < 0 || userToFollowID < 0)
            {
                return NotAcceptable;
            }

            if (userInSessionID == userToFollowID)
            {
                return NotAcceptable;
            }

            var follow = new Follower {
                who_id = userInSessionID,
                whom_id = userToFollowID
            };

            var entityAlreadyExists = _context.Follower.Find(userInSessionID, userToFollowID);
            if (entityAlreadyExists != null)
            {
                return NotAcceptable;
            }

            _context.Follower.Add(follow);
            _context.SaveChanges();

            return NoContent;
        }

        public HttpStatusCode UnfollowUser(string usernameInSession, string usernameToUnfollow)
        {
            var userInSessionID = userRepo.GetUserIDFromUsername(usernameInSession);
            var userToUnfollowID = userRepo.GetUserIDFromUsername(usernameToUnfollow);

            if (userInSessionID < 0 || userToUnfollowID < 0)
            {
                // Console.WriteLine("1");
                return NotAcceptable;
            }

            // var follower = _context.Follower.Where(x => x.who_id == userInSessionID && x.whom_id == userToUnfollowID)
            //                                  .FirstOrDefault<Follower>();

            var follower = _context.Follower.Find(userInSessionID, userToUnfollowID);

            if (follower == null)
            {
                // Console.WriteLine("2");
                return NoContent;
            }

            // Console.WriteLine("3");
            _context.Follower.Remove(follower);
            _context.SaveChanges();

            return NoContent;
        }

        public HttpStatusCode GetFollowRelation(string who, string whom)
        {
            var _who = userRepo.GetUserIDFromUsername(who);
            var _whom = userRepo.GetUserIDFromUsername(whom);
            var follow = _context.Follower.Find(_who, _whom);

            if (follow == null)
            {
                return NotFound;
            }

            return NoContent;
        }
        
        public bool DoesUserFollow(int user_id, int isFollowed_id)
        {
            if ((from f in _context.Follower
                 where f.who_id == user_id &&
                 f.whom_id == isFollowed_id
                 select f.who_id).ToList().Count > 0) return true;

            else return false;

        }

        // TODO : method for retriveing a collection of all the follower given a who_id and vice versa 
    
    }
}