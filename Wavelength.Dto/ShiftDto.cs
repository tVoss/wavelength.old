using System;
using System.Collections.Generic;
using System.Text;

namespace Wavelength.Dto
{
    public class ShiftDto
    {
        public int BarId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ProfileDto Profile { get; set; }
    }
}
