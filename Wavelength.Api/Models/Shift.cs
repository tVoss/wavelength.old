using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
}
