using GrapePhoto.Proxy;
using GrapePhoto.Web.Models;
using GrapePhoto.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Models
{
    public class ProfileIndexModel
    {

        public User User { get; set; }

        public Relationship Relationship { get; set; }

        public IPagedList<PostDto> Posts { get; set; }

        public int Followers { get; set; }
        public int Followees { get; set; }

        IAccountClient _accountClient;

        public ProfileIndexModel(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }
        public void SetRelationship(string currentUserId)
        {
            if (User.UserId == currentUserId)
            {
                Relationship = Relationship.Self;
            }
            else
            {
                var currentUsersfollowers = _accountClient.GetAllFollowersUsersByUserId(currentUserId);
               
                var currentUsersfollowee = _accountClient.GetAllFollowingUsersByUserId(currentUserId);


                var usersFollowers = _accountClient.GetAllFollowersUsersByUserId(User.UserId);
                var usersFollowees = _accountClient.GetAllFollowingUsersByUserId(User.UserId);

                Followers = usersFollowers.Count();
                Followees = usersFollowees.Count();

                if (currentUsersfollowers.Any(x => x.UserId == User.UserId) && currentUsersfollowee.Any(x => x.UserId == User.UserId))
                {
                    Relationship = Relationship.FollowingEachOther;
                }
                else if (!currentUsersfollowee.Any(x => x.UserId == User.UserId))
                {
                    Relationship = Relationship.Unfollowed;
                }
                else if (currentUsersfollowee.Any(x => x.UserId == User.UserId))
                {
                    Relationship = Relationship.Following;
                }
            }
        }
    }
}
