using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Newtonsoft.Json;

namespace GrapePhoto.Proxy
{
    public static class PostAPI
    {
        public static string AddPost => $"/post/add";
        public static string GetRecentPost => $"/post/recent";

       
    }

    public class PostService : IPostService
    {
        private string _baseUri;
        private IRestClient _client;
        public PostService(string baseUri)
        {
            _baseUri = baseUri;
            _client = new RestClient(_baseUri);
        }
        public PostDto AddPost(PostDto postDto)
        {
            var request = new RestSharp.RestRequest(PostAPI.AddPost)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
          
            request.AddJsonBody(postDto);

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                var users = JsonConvert.DeserializeObject<PostDto>(json.result.ToString());
                return users;
            }
            else
            {
                return null;
            }
        }
    }
}
