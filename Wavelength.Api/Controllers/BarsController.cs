using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wavelength.Api.Models;
using Wavelength.Dto;

namespace Wavelength.Api.Controllers
{
    [Route("api/bars")]
    public class BarsController : WavelengthController
    {

        #region Reports

        [HttpGet]
        [Route("{id}/report")]
        public async Task<IActionResult> GetReport(int id)
        {
            if (await DbContext.Bars.FindAsync(id) == null)
            {
                return NotFound();
            }

            var reportStart = DateTime.UtcNow - TimeSpan.FromHours(2);
            var reports = await DbContext.BarReports.Where(r => r.Bar.Id == id && r.ReportedOn > reportStart).ToListAsync();

            var cover = reports
                .Select(r => r.Cover)
                .Where(c => c != null)
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .First()
                .Key.Value;

            var line = reports.Select(r => r.Line).Where(l => l != null).Sum().Value / reports.Where(r => r.Line != null).Count();
            var capacity = reports.Select(r => r.Capacity).Where(c => c != null).Sum().Value / reports.Where(r => r.Capacity != null).Count();

            var report = new BarReportDto
            {
                Cover = cover,
                Line = line,
                Capacity = capacity
            };

            return Ok(report);
        }

        [HttpPost]
        [Route("{id}/report")]
        public async Task<IActionResult> PostReport(int id, [FromBody] BarReportDto dto)
        {
            var bar = await DbContext.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }

            var report = new BarReport
            {
                Bar = bar,
                ReportedBy = null,
                ReportedOn = DateTime.UtcNow,
                Cover = dto.Cover,
                Line = dto.Line,
                Capacity = dto.Capacity
            };

            DbContext.BarReports.Add(report);
            await DbContext.SaveChangesAsync();

            return Ok();
        }

        #endregion

        #region Tenders

        [HttpGet]
        [Route("{id}/tenders")]
        public async Task<IActionResult> GetTenders(int id)
        {
            // All shifts at the bar after right now
            var tenders = await DbContext.Shifts.Where(s => s.Bar.Id == id && s.Start > DateTime.Now).Take(50).ToArrayAsync();
            
        }

        #endregion

    }
}
