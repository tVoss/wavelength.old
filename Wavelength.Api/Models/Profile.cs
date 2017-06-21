using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Dto;

namespace Wavelength.Api.Models
{
    public class Profile
    {
        [Key]
        public Guid Id { get; set; }
        public string FacebookId { get; set; }
        public string Bio { get; set; }
        public bool IsTender { get; set; }
        public virtual Bar FavoriteBar { get; set; }
        public virtual List<Shift> Shifts { get; set; }
    }

    public static class ProfileExtensions
    {
        public static ProfileDto ToProfileDto(this Profile profile)
        {
            return new ProfileDto
            {
                Name = "asdf",
                Bio = profile.Bio,
                PhotoUrl = "asdf",
                FavoriteBarId = profile.FavoriteBar.Id
            };
        }
    }
}
