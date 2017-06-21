using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Wavelength.Api.Models;
using Wavelength.Dto;

namespace Wavelength.Api
{
    public class FacebookApi
    {
        private RestClient _restClient;

        public FacebookApi()
        {
            _restClient = new RestClient("https://graph.facebook.com/v2.9/");
        }

        public async Task<FacebookProfileDto> GetUserProfile(string accessToken)
        {
            var req = new RestRequest("me", Method.GET);
            req.AddQueryParameter("access_token", accessToken);

            var source = new TaskCompletionSource<FacebookProfileDto>();
            _restClient.ExecuteAsync<FacebookProfileDto>(req, res =>
            {
                source.SetResult(res.StatusCode == HttpStatusCode.OK ? res.Data : null);
            });

            return await source.Task;
        }

        public async Task<string[]> GetUserFriends(string accessToken)
        {
            return await Task.FromResult(new string[] { });
        }

    }
}
