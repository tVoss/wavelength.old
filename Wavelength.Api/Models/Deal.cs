using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wavelength.Api.Models
{
    public class Deal
    {
        [Key]
        public int Id { get; set; }
        public virtual Bar Bar { get; set; }
        public bool Weekly { get; set; }
        public bool Active { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
