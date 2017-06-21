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
        public BarsController(WavelengthDbContext context, FacebookApi api) : base(context, api)
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetBars()
        {
            var bars = await DbContext.Bars.ToListAsync();

            return Ok(bars.Select(b => b.ToDto()));
        }
        

        #region Reports

        [HttpGet]
        [Route("reports")]
        public async Task<IActionResult> GetReports()
        {
            var bars = await DbContext.Bars.ToListAsync();

            var reportStart = DateTime.UtcNow - TimeSpan.FromHours(2);
            return Ok(bars.Select(b => b.ReportSince(reportStart).ToDto()));
        }

        [HttpGet]
        [Route("{id}/report")]
        public async Task<IActionResult> GetReport(int id)
        {
            Bar bar;
            if ((bar = await DbContext.Bars.FindAsync(id)) == null)
            {
                return NotFound();
            }

            var reportStart = DateTime.UtcNow - TimeSpan.FromHours(2);
            return Ok(bar.ReportSince(reportStart).ToDto());
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
            if (await DbContext.Bars.FindAsync(id) == null)
            {
                return NotFound();
            }

            // Start time is right now
            var start = DateTime.UtcNow;
            // If it's between 12am and 2am, add 2 hours to the date, otherwise 26 (1 day + 2 hours)
            var end = start.Date + TimeSpan.FromHours(start.TimeOfDay.Hours < 2 ? 2 : 26);
            var friends = await FacebookApi.GetUserFriends(User.Claims.Single(c => c.Type == "token").Value);

            var shifts = await DbContext.Shifts.Where(s =>
                s.Bar.Id == id && 
                s.Start < end &&
                s.End > start &&
                friends.Contains(s.Profile.FacebookId))
                .ToArrayAsync();

            return Ok(shifts.Select(s => s.ToDto()));
        }

        #endregion

    }
}
