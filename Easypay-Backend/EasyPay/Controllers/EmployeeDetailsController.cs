using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyPay.Models;
using EasyPay.DTO;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Controllers
{
    [Authorize] // Ensure only authenticated users can access
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {
        private readonly PayrollContext _context;

        public EmployeeDetailsController(PayrollContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeDetails
        [HttpGet]
        public async Task<ActionResult<EmployeeDetailsDto>> GetEmployeeDetails()
        {
            try
            {
                // Extract the Employee's email from the JWT claims
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not authorized.");
                }

                // Find the employee based on the authenticated user's email
                var employee = await _context.Employees
                    .Include(e => e.EmployeeDetails)
                    .FirstOrDefaultAsync(e => e.Email == email);

                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                // If the employee's details are missing, return a placeholder response
                if (employee.EmployeeDetails == null)
                {
                    return Ok(new { message = "Employee details are not present. You can add them." });
                }

                // Map the EmployeeDetails to DTO without the photo
                var employeeDetailsDto = new EmployeeDetailsDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    Designation = employee.Designation,
                    Email = employee.Email,
                    Department = employee.Department,
                    Address = employee.EmployeeDetails.Address,
                    MobileNo = employee.EmployeeDetails.MobileNo,
                    BloodGroup = employee.EmployeeDetails.BloodGroup
                };

                return Ok(employeeDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/EmployeeDetails
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeDetails([FromBody] GetEmployeeDetailsDTO employeeDetailsDto)
        {
            try
            {
                // Ensure that the request is coming from the authenticated user
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not authorized.");
                }

                // Find the employee based on the authenticated user's email
                var employee = await _context.Employees
                    .Include(e => e.EmployeeDetails)
                    .FirstOrDefaultAsync(e => e.Email == email);

                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                // Check if the employee already has details
                if (employee.EmployeeDetails == null)
                {
                    // Create new EmployeeDetails if they don't exist
                    employee.EmployeeDetails = new EmployeeDetails
                    {
                        EmployeeId = employee.EmployeeId,
                        Address = employeeDetailsDto.Address,
                        MobileNo = employeeDetailsDto.MobileNo,
                        BloodGroup = employeeDetailsDto.BloodGroup
                    };

                    _context.EmployeeDetails.Add(employee.EmployeeDetails);  // Add new details to the context
                }
                else
                {
                    // Update existing EmployeeDetails
                    employee.EmployeeDetails.Address = employeeDetailsDto.Address;
                    employee.EmployeeDetails.MobileNo = employeeDetailsDto.MobileNo;
                    employee.EmployeeDetails.BloodGroup = employeeDetailsDto.BloodGroup;

                    _context.Entry(employee.EmployeeDetails).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                return Ok("Employee details updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Could not update employee details.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        // POST: api/EmployeeDetails
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeDetails([FromBody] GetEmployeeDetailsDTO employeeDetailsDto)
        {
            try
            {
                // Ensure that the request is coming from the authenticated user
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized("User is not authorized.");
                }

                // Find the employee based on the authenticated user's email
                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Email == email);

                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                // Create a new EmployeeDetails instance and map the properties
                var employeeDetails = new EmployeeDetails
                {
                    EmployeeId = employee.EmployeeId,
                    Address = employeeDetailsDto.Address,
                    MobileNo = employeeDetailsDto.MobileNo,
                    BloodGroup = employeeDetailsDto.BloodGroup
                };

                // Add the new EmployeeDetails to the database
                _context.EmployeeDetails.Add(employeeDetails);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployeeDetails), new { id = employeeDetails.EmployeeDetailsId }, employeeDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
