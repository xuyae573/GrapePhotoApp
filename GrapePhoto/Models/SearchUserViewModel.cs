using GrapePhoto.Proxy;
using GrapePhoto.Web.Models.Account;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Models
{
    public class SearchUserViewModel
    {
        private IAccountClient _accountClient;

        public User User { get; set; }

        public Relationship Relationship { get; set; }


        public SearchUserViewModel(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }
        //#relationship

        public void GetRelationship(string currentUserId)
        {

            if (User.UserId == currentUserId)
            {
                Relationship = Relationship.Self;
            }
            else
            {
                var currentUsersfollowers = _accountClient.GetAllFollowersUsersByUserId(currentUserId);
                var currentUsersfollowee = _accountClient.GetAllFollowingUsersByUserId(User.UserId);

                if (currentUsersfollowers.Any(x => x.UserId == User.UserId) && currentUsersfollowee.Any(x => x.UserId == User.UserId))
                {
                    Relationship = Relationship.FollowingEachOther;
                }
                else if(!currentUsersfollowee.Any(x => x.UserId == User.UserId))
                {
                    Relationship = Relationship.Unfollowed;
                }
                else if(currentUsersfollowee.Any(x => x.UserId == User.UserId))
                {
                    Relationship = Relationship.Following;
                }
            }
        }
    }


    public enum Relationship
    {
        Self,
        Unfollowed,
        Following,
        FollowingEachOther
    } 
}
