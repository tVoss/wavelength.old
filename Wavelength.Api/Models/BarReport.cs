using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wavelength.Api.Models
{
    public class BarReport
    {
        [Key]
        public int Id { get; set; }
        public virtual Bar Bar { get; set; }
        public virtual Profile ReportedBy { get; set; }
        public DateTime ReportedOn { get; set; }
        public int? Cover { get; set; }
        public float? Line { get; set; }
        public float? Capacity { get; set; }
    }
}
