using EasyPay.DTO;
using EasyPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PayrollContext _context;

        public EmployeeController(PayrollContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            return await _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    Designation = e.Designation,
                    Email = e.Email,
                    DateOfJoining = e.DateOfJoining,
                    Department = e.Department,
                    BasicSalary = e.BasicSalary,
                    BankName = e.BankName,
                    AccountNumber = e.AccountNumber,
                    GradeId = e.GradeId
                })
                .ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    Designation = e.Designation,
                    Email = e.Email,
                    DateOfJoining = e.DateOfJoining,
                    Department = e.Department,
                    BasicSalary = e.BasicSalary,
                    BankName = e.BankName,
                    AccountNumber = e.AccountNumber,
                    GradeId = e.GradeId
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // POST: api/Employee
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                EmployeeName = employeeDto.EmployeeName,
                Designation = employeeDto.Designation,
                Email = employeeDto.Email,
                DateOfJoining = employeeDto.DateOfJoining,
                Department = employeeDto.Department,
                BasicSalary = employeeDto.BasicSalary,
                BankName = employeeDto.BankName,
                AccountNumber = employeeDto.AccountNumber,
                GradeId = employeeDto.GradeId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeName = employeeDto.EmployeeName;
            employee.Designation = employeeDto.Designation;
            employee.Email = employeeDto.Email;
            employee.DateOfJoining = employeeDto.DateOfJoining;
            employee.Department = employeeDto.Department;
            employee.BasicSalary = employeeDto.BasicSalary;
            employee.BankName = employeeDto.BankName;
            employee.AccountNumber = employeeDto.AccountNumber;
            employee.GradeId = employeeDto.GradeId;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var usersToDelete = await _context.Users.Where(u => u.EmployeeId == id).ToListAsync();
            _context.Users.RemoveRange(usersToDelete);
            await _context.SaveChangesAsync(); // Save changes

            // Now delete the employee
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
