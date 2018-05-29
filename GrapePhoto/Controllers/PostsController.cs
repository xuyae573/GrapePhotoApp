using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PusherServer;
using GrapePhoto.Helper;
using GrapePhoto.Models;

namespace GrapePhoto.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            //Get current user
            //Get all the posts of this user and return
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string UserName, String Content)
        {
            if (ModelState.IsValid)
            {
                //_context.Products.Add(product);
                //await _context.SaveChangesAsync();
                //Call lambdy to save post into database


                //Read dynamo db to get the message to display

                //Get result from database , inluce the follower ID
                //var data = new
                //{
                //    message = System.String.Format("{0} post a image", UserName),
                //    url = @"http://news.bbcimg.co.uk/media/images/71977000/jpg/_71977649_71825880.jpg\"
                //};

                var m = new Message()
                {
                    UserName = UserName,
                    Content = Content,
                    ImageUrl = @"http://news.bbcimg.co.uk/media/images/71977000/jpg/_71977649_71825880.jpg",
                    Followers = new string[] { "liucao", "allen" }
                };


                // context.indentity.userid  is in the list of database 

                await Channel.Trigger(m, "feed", "new_feed");

            }

            return RedirectToAction(nameof(Index));
        }
    }
}