using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Api.Models;

namespace Wavelength.Api
{
    public class FacebookApi
    {

        public async Task<string[]> GetUserFriends(string accessToken)
        {
            return await Task.FromResult(new string[] { });
        }

        public async Task<bool> UsersAreFriends(string userIdA, string userIdB, string accessToken)
        {
            return await Task.FromResult(true);
        }

    }
}
