using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Dto;

namespace Wavelength.Api.Models
{
    public class Bar
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public virtual List<Deal> Deals { get; set; }
        public virtual List<BarReport> Reports { get; set; }

        public BarReport ReportSince(DateTime since)
        {
            // Get reports after the time frame
            var reports = Reports.Where(r => r.ReportedOn > since);

            var cover = reports
                // Focus on the cover
                .Select(r => r.Cover)
                // Make sure it's not null
                .Where(c => c != null)
                // Group by value
                .GroupBy(c => c)
                // Order by size
                .OrderByDescending(g => g.Count())
                // Take the mode
                .First()
                // And get the value
                .Key.Value;

            var line = reports
                // Focus on the line
                .Select(r => r.Line)
                // Make sure it's not null
                .Where(l => l != null)
                // Take the sum
                .Sum()
                // Then the average
                .Value / reports.Where(r => r.Line != null).Count();

            var capacity = reports
                // Focus on the capacity
                .Select(r => r.Capacity)
                // Make sure it's not null
                .Where(c => c != null)
                // Take the sum
                .Sum()
                // Then the average
                .Value / reports.Where(r => r.Capacity != null).Count();
            
            return new BarReport
            {
                Bar = this,
                Cover = cover,
                Line = line,
                Capacity = capacity,
                ReportedOn = DateTime.UtcNow
            };
        }
    }

    public static class BarExtensions
    {
        public static BarDto ToDto(this Bar bar)
        {
            return new BarDto
            {
                Id = bar.Id,
                Name = bar.Name,
                LogoUrl = bar.LogoUrl
            };
        }
    }
}
