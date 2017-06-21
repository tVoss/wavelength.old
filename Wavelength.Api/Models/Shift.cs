using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Dto;

namespace Wavelength.Api.Models
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Bar Bar { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public static class ShiftExtensions
    {
        public static ShiftDto ToDto(this Shift shift)
        {
            return new ShiftDto
            {
                BarId = shift.Bar.Id,
                Start = shift.Start,
                End = shift.End,
                Profile = shift.Profile.ToProfileDto()
            };
        }
    }
}
