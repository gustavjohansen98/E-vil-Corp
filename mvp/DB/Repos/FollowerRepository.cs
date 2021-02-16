using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Minitwit.Entities;
using static System.Net.HttpStatusCode;

namespace Repos
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
                return NotFound;

            var follow = new Follower {
                who_id = userInSessionID,
                whom_id = userToFollowID
            };

            var entityAlreadyExists = _context.Follower.Find(userInSessionID, userToFollowID);
            if (entityAlreadyExists != null)
                return NotAcceptable;

            _context.Follower.Add(follow);
            _context.SaveChanges();

            return NoContent;
        }

        public HttpStatusCode UnfollowUser(string usernameInSession, string usernameToUnfollow)
        {
            var userInSessionID = userRepo.GetUserIDFromUsername(usernameInSession);
            var userToUnfollowID = userRepo.GetUserIDFromUsername(usernameToUnfollow);

            if (userInSessionID < 0 || userToUnfollowID < 0)
                return NotFound;

            // var follower = _context.Follower.Where(x => x.who_id == userInSessionID && x.whom_id == userToUnfollowID)
            //                                  .FirstOrDefault<Follower>();

            var follower = _context.Follower.Find(userToUnfollowID, userInSessionID);

            if (follower == null)
                return NotAcceptable;
                                            
            _context.Follower.Remove(follower);
            _context.SaveChanges();

            return NoContent;
        }


        // TODO : method for retriveing a collection of all the follower given a who_id and vice versa 
    
    }
}