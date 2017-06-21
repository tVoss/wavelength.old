using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Api.Models;
using Wavelength.Dto;
using Wavelength.Dto.Request;

namespace Wavelength.Api.Controllers
{

    [Route("api/profile")]
    public class ProfileController : WavelengthController
    {

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProfile()
        {
            string facebookId = null; // Get this?

            var profile = await DbContext.Profiles.SingleOrDefaultAsync(p => p.FacebookId == facebookId);

            return profile == null ? (IActionResult)NotFound() : Ok(profile);
        }

        [HttpGet]
        [Route("friends")]
        public async Task<IActionResult> GetFriends()
        {
            string facebookId = null;
            var friends = await FacebookApi.GetUserFriends(null);

            var profiles = await DbContext.Profiles.Where(p => friends.Contains(p.FacebookId)).ToArrayAsync();

            return Ok(profiles.Select(p => p.ToProfileDto()));
        }

        [HttpGet]
        [Route("tenders")]
        public async Task<IActionResult> GetTenders()
        {
            var friends = await FacebookApi.GetUserFriends(null);

            var tenders = await DbContext.Profiles.Where(p => p.IsTender && friends.Contains(p.FacebookId)).ToArrayAsync();

            return Ok(tenders.Select(p => p.ToProfileDto()));
        }

        [HttpGet]
        [Route("friends/{id}")]
        public async Task<IActionResult> GetFriend(Guid id)
        {
            var friend = await DbContext.Profiles.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            
            if (!await FacebookApi.UsersAreFriends(null, friend.FacebookId, null))
            {
                return Forbid();
            }

            return Ok(friend.ToProfileDto());
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateProfile([FromBody] NewProfileRequest dto)
        {
            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                FacebookId = null,
                FavoriteBar = await DbContext.Bars.FindAsync(dto.FavoriteBarId),
                Bio = dto.Bio
            };

            DbContext.Profiles.Add(profile);
            await DbContext.SaveChangesAsync();

            return Ok(profile);
        }

    }
}
