﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using catch_the_trash.Data;
using catch_the_trash.Models;
using Microsoft.AspNetCore.Cors;
using System.Net;
using catch_the_trash.Utils;

namespace catch_the_trash.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ReportsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET api/Reports
        [HttpGet("numberOfReports")]
        public async Task<ActionResult> GetNumberOfRows()
        {
            var reports = await _context.Report.ToListAsync();
            var numberOfReports = reports.Count;
            return Ok(numberOfReports);
        }


        // GET: api/Reports?page="pageNumber"&size="numberOfRowsPerPage"
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportModel>>> GetReport([FromQuery(Name ="page")] int pageNumber, [FromQuery(Name ="size")] int pageSize)
        {
            var reports = await PaginatedList<ReportModel>.CreateAsync(_context.Report.AsNoTracking(), pageNumber, pageSize);
            foreach(var report in reports)
            {
                report.Images = _context.Image.Where(i => i.Report.Id == report.Id).Select(i => new ImageModel { Id = i.Id, ImageName = i.ImageName }).ToList();
            }
            return reports;
        }


        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportModel>> GetReport(int id)
        {
            var report = await _context.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }
            report.Images = _context.Image.Where(i => i.Report.Id == id).Select(i => new ImageModel { Id = i.Id, ImageName = i.ImageName }).ToList();
            return report;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, ReportModel report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reports
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReportModel>> PostReport(ReportModel report)
        {
            _context.Report.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReportModel>> DeleteReport(int id)
        {
            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Report.Remove(report);
            await _context.SaveChangesAsync();

            return report;
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
