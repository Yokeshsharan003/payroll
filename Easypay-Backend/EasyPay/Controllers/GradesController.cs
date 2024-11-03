using EasyPay.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPay.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyPay.Controllers
{
 

    
        [Route("api/[controller]")]
        [ApiController]
        public class GradeController : ControllerBase
        {
            private readonly PayrollContext _context;

            public GradeController(PayrollContext context)
            {
                _context = context;
            }

            // GET: api/Grade
            [HttpGet]
            [Authorize(Roles = "Admin,PayrollProcessor")]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetGrades()
            {
                var grades = await _context.Grades
                    .Include(g => g.PayrollPolicy)
                    .Include(g => g.Employees)
                    .Select(g => new GradeDto
                    {
                        GradeId = g.GradeId,
                        GradeName = g.GradeName,
                        PayrollPolicyId = g.PayrollPolicyId,
                        
                    }).ToListAsync();

                return Ok(grades);
            }

            // GET: api/Grade/5
            [HttpGet("{id}")]
            [Authorize(Roles = "Admin,PayrollProcessor")]
        public async Task<ActionResult<GradeDto>> GetGrade(int id)
            {
                var grade = await _context.Grades
                    .Include(g => g.PayrollPolicy)
                    .Include(g => g.Employees)
                    .Where(g => g.GradeId == id)
                    .Select(g => new GradeDto
                    {
                        GradeId = g.GradeId,
                        GradeName = g.GradeName,
                        PayrollPolicyId = g.PayrollPolicyId,
                        
                    }).FirstOrDefaultAsync();

                if (grade == null)
                {
                    return NotFound();
                }

                return Ok(grade);
            }

            // POST: api/Grade
            [HttpPost]
            [Authorize(Roles = "Admin,PayrollProcessor")]
            public async Task<ActionResult<GradeDto>> PostGrade(GradeDto gradeDto)
            {
                var grade = new Grade
                {
                    GradeName = gradeDto.GradeName,
                    PayrollPolicyId = gradeDto.PayrollPolicyId
                };

                _context.Grades.Add(grade);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetGrade), new { id = grade.GradeId }, gradeDto);
            }

            // PUT: api/Grade/5
            [HttpPut("{id}")]
            [Authorize(Roles = "Admin,PayrollProcessor")]
            public async Task<IActionResult> PutGrade(int id, GradeDto gradeDto)
            {
                if (id != gradeDto.GradeId)
                {
                    return BadRequest();
                }

                var grade = await _context.Grades.FindAsync(id);
                if (grade == null)
                {
                    return NotFound();
                }

                grade.GradeName = gradeDto.GradeName;
                grade.PayrollPolicyId = gradeDto.PayrollPolicyId;

                _context.Entry(grade).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(id))
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

            // DELETE: api/Grade/5
            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin,PayrollProcessor")]
            public async Task<IActionResult> DeleteGrade(int id)
            {
                var grade = await _context.Grades.FindAsync(id);
                if (grade == null)
                {
                    return NotFound();
                }

                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool GradeExists(int id)
            {
                return _context.Grades.Any(g => g.GradeId == id);
            }
        }
    }


