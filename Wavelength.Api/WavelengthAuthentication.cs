using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace Wavelength.Api
{

    public class WavelengthAuthenticationOptions : AuthenticationOptions
    {

    }

    public class WavelengthAuthenticationMiddleware : AuthenticationMiddleware<WavelengthAuthenticationOptions>
    {

        public WavelengthAuthenticationMiddleware(RequestDelegate next, IOptions<WavelengthAuthenticationOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder) : base(next, options, loggerFactory, encoder)
        {
        }

        protected override AuthenticationHandler<WavelengthAuthenticationOptions> CreateHandler()
        {
            return new WavelengthAuthenticationaHandler();
        }
    }

    public class WavelengthAuthenticationaHandler : AuthenticationHandler<WavelengthAuthenticationOptions>
    {
        private FacebookApi _facebookApi = new FacebookApi();

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader)) {
                return AuthenticateResult.Fail("Missing or malformed 'Authorization' header");
            }

            var authValues = authHeader.First().Split(' ');
            if (authValues.Length != 2 || !authValues[0].Equals("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid authentication scheme");
            }

            var profile = await _facebookApi.GetUserProfile(authValues[1]);

            if (profile == null)
            {
                return AuthenticateResult.Fail("Invalid access token");
            }

            var identity = new ClaimsIdentity("Facebook");
            identity.AddClaim(new Claim("id", profile.Id));
            identity.AddClaim(new Claim("name", profile.Name));
            identity.AddClaim(new Claim("birthday", profile.Birthday));
            identity.AddClaim(new Claim("email", profile.Email));
            identity.AddClaim(new Claim("verified", profile.Verified.ToString()));

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "Facebook");
            return AuthenticateResult.Success(ticket);
        }
    }
}
