using GrapePhoto.Proxy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Views.Shared.ViewComponents
{
    public class PopularUserComponent: ViewComponent
    {

        private readonly IAccountClient _accountClient;

        public PopularUserComponent(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }


        public IViewComponentResult Invoke(int numberOfItems)
        {
            var users = _accountClient.GetPopularUsers(numberOfItems);
 
            users = users.Where(x => x.UserId != HttpContext.User.Identity.Name).ToList();
            
            return View(users);
        }

    }
}
