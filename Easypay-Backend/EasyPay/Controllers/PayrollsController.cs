using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using EasyPay.Service;
using EasyPay.DTO;
using Microsoft.AspNetCore.Authorization;

namespace EasyPay.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost("generate")]
        [Authorize(Roles = "Admin,PayrollProcessor")]
        public async Task<IActionResult> GeneratePayroll([FromBody] GeneratePayrollRequest request)
        {
            try
            {
                var payrollDetails = await _payrollService.GeneratePayrollAsync(request.EmployeeId, request.PayDate);
                return Ok(payrollDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("netpay/{payrollId}")]
        [Authorize(Roles = "Admin,PayrollProcessor")]
        public IActionResult GetNetPay(int payrollId)
        {
            try
            {
                var netPay = _payrollService.CalculateNetPay(payrollId);
                return Ok(new { NetPay = netPay });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("verify/{payrollId}")]
        [Authorize(Roles = "Admin,PayrollProcessor")]
        public IActionResult VerifyPayroll(int payrollId)
        {
            try
            {
                var isValid = _payrollService.VerifyPayrollData(payrollId);
                return Ok(new { IsValid = isValid });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("department/{department}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetDepartmentPayrolls(string department)
        {
            try
            {
                var payrolls = await _payrollService.ReviewDepartmentPayrollsAsync(department);
                return Ok(payrolls);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}