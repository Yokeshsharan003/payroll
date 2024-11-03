using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollPoliciesController : ControllerBase
    {
        private readonly PayrollContext _context;

        public PayrollPoliciesController(PayrollContext context)
        {
            _context = context;
        }

        // GET: api/PayrollPolicies
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PayrollPolicy>>> GetPayrollPolicies()
        {
            return await _context.PayrollPolicies.ToListAsync();
        }

        // GET: api/PayrollPolicies/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PayrollPolicy>> GetPayrollPolicy(int id)
        {
            var payrollPolicy = await _context.PayrollPolicies.FindAsync(id);

            if (payrollPolicy == null)
            {
                return NotFound();
            }

            return payrollPolicy;
        }

        // PUT: api/PayrollPolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPayrollPolicy(int id, PayrollPolicy payrollPolicy)
        {
            if (id != payrollPolicy.PayrollPolicyId)
            {
                return BadRequest();
            }

            _context.Entry(payrollPolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayrollPolicyExists(id))
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

        // POST: api/PayrollPolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PayrollPolicy>> PostPayrollPolicy(PayrollPolicy payrollPolicy)
        {
            _context.PayrollPolicies.Add(payrollPolicy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayrollPolicy", new { id = payrollPolicy.PayrollPolicyId }, payrollPolicy);
        }

        // DELETE: api/PayrollPolicies/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePayrollPolicy(int id)
        {
            var payrollPolicy = await _context.PayrollPolicies.FindAsync(id);
            if (payrollPolicy == null)
            {
                return NotFound();
            }

            _context.PayrollPolicies.Remove(payrollPolicy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayrollPolicyExists(int id)
        {
            return _context.PayrollPolicies.Any(e => e.PayrollPolicyId == id);
        }
    }
}
