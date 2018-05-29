using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models;
using GrapePhoto.Web.Models.Posts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.Newtonsoft.Json;

namespace GrapePhoto.Proxy
{
    public static class PostAPI
    {
        public static string AddPost => $"/post/add";
        public static string GetRecentPost => $"/post/recent";
        public static string GetUsersPost => $"/post/my";
        public static string Feed => $"/post/feeds";

        public static string Like => $"/post/like";
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
                postDto = JsonConvert.DeserializeObject<PostDto>(json.result.ToString());
              
                return postDto;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get all following posts
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<PostDto> GetFollowingPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var items = new List<PostDto>();

            var request = new RestSharp.RestRequest(PostAPI.Feed)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var limits = (pageIndex + 1) * pageSize;
            request.AddJsonBody(new{ UserId = userId,Limit = limits});
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                items = JsonConvert.DeserializeObject<List<PostDto>>(json.result.ToString());
            }
            return new PagedList<PostDto>(items, pageIndex, pageSize);
        }

        /// <summary>
        /// Get All User's posts except user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<PostDto> GetPosts(string userId, int pageIndex=0, int pageSize = int.MaxValue)
        {
            var request = new RestSharp.RestRequest(PostAPI.GetRecentPost)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var limits = (pageIndex + 1) * pageSize;
            request.AddJsonBody(new { UserId = userId, Limit = limits });

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);


            if (json.success)
            {
                List<PostDto> items = new List<PostDto>();
                items = JsonConvert.DeserializeObject<List<PostDto>>(json.result.ToString());
                return new PagedList<PostDto>(items, pageIndex, pageSize);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get user's posts
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<PostDto> GetUserPostsByUserId(string userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var items = new List<PostDto>();

            var request = new RestSharp.RestRequest(PostAPI.GetUsersPost)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var limits = (pageIndex + 1) * pageSize;
            request.AddJsonBody(new { UserId = userId, Limit = limits });
    
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                items = JsonConvert.DeserializeObject<List<PostDto>>(json.result.ToString());
            }
            return new PagedList<PostDto>(items, pageIndex, pageSize);
        }

        public PostDto LikePost(LikePostDto likePostDto)
        {
            var request = new RestSharp.RestRequest(PostAPI.Like)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };

            request.AddJsonBody(likePostDto);

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);

            if (json.success)
            {
               var postDto = JsonConvert.DeserializeObject<PostDto>(json.result.ToString());
               return postDto;
            }
            else
            {
                return null;
            }
        }

        public PostDto UnlikePost(LikePostDto likePostDto)
        {
            throw new NotImplementedException();
        }
    }
}
