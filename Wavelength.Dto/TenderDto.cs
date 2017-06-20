using System;
using System.Collections.Generic;
using System.Text;

namespace Wavelength.Dto
{
    public class TenderDto : ProfileDto
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
