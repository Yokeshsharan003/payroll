using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using Microsoft.AspNetCore.Authorization;
using EasyPay.DTO;

namespace EasyPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplianceReportsController : ControllerBase
    {
        private readonly PayrollContext _context;

        public ComplianceReportsController(PayrollContext context)
        {
            _context = context;
        }

        // GET: api/ComplianceReports
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ComplianceReport>>> GetComplianceReports()
        {
            return await _context.ComplianceReports.ToListAsync();
        }

        // GET: api/ComplianceReports/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ComplianceReport>> GetComplianceReport(int id)
        {
            var complianceReport = await _context.ComplianceReports.FindAsync(id);

            if (complianceReport == null)
            {
                return NotFound();
            }

            return complianceReport;
        }

        // PUT: api/ComplianceReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutComplianceReport(int id, ComplianceReport complianceReport)
        {
            if (id != complianceReport.CRId)
            {
                return BadRequest();
            }

            _context.Entry(complianceReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplianceReportExists(id))
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

        // POST: api/ComplianceReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ComplianceReport>> PostComplianceReport(ComlianceReportDTO complianceReport)
        {

            var comp = new ComplianceReport
            {
                
                ReportName = complianceReport.ReportName,
                Description= complianceReport.Description,
                CreatedDate = DateTime.Now
            };
            _context.ComplianceReports.Add(comp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplianceReport", new { id = comp.CRId }, comp);
        }

        // DELETE: api/ComplianceReports/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComplianceReport(int id)
        {
            var complianceReport = await _context.ComplianceReports.FindAsync(id);
            if (complianceReport == null)
            {
                return NotFound();
            }

            _context.ComplianceReports.Remove(complianceReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplianceReportExists(int id)
        {
            return _context.ComplianceReports.Any(e => e.CRId == id);
        }
    }
}
