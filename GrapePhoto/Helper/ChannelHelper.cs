using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using GrapePhoto.Models;

namespace GrapePhoto.Helper
{
    public class Channel
    {
        public static async Task<IActionResult> Trigger(Message m, string channelName, string eventName)
        {
            var options = new PusherOptions
            {
                Cluster = "ap1",
                Encrypted = true
            };
            var pusher = new Pusher(
              "532674",
              "c5705e0b64eddb898170",
              "766ef1c2af1bd086ecb5",
              options
            );

            var result = await pusher.TriggerAsync(
              channelName,
              eventName,
              m
            );
            return new OkObjectResult(m);
        }
    }
}
