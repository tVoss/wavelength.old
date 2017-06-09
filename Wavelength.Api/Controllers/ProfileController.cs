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
