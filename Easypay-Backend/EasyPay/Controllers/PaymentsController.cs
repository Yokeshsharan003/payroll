using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using EasyPay.Service;
using Microsoft.AspNetCore.Authorization;
using EasyPay.DTO;

namespace EasyPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        //// Endpoint to process payments
        //[HttpPost("process")]
        //[Authorize(Roles = "PayrollProcessor")]
        //public async Task<IActionResult> ProcessPayment(int employeeId, decimal amount, DateTime paymentDate, string paymentMethod)
        //{
        //    try
        //    {
        //        // Call the service layer to process the payment
        //        await _paymentService.ProcessPaymentAsync(employeeId, amount, paymentDate, paymentMethod);

        //        // Return success message
        //        return Ok("Payment processed successfully.");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        // Return bad request with the exception message
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any other general exceptions
        //        return StatusCode(500, new { message = "An error occurred while processing the payment.", details = ex.Message });
        //    }
        //}

        // Endpoint to view pay stubs
        [HttpGet("view")]
        [Authorize(Roles = "PayrollProcessor,Employee,Admin,Manager")]
        public async Task<IActionResult> GetPayStubsAsync(int employeeId)
        {
            try
            {
                // Call the service layer to fetch pay stubs
                var payStubs = await _paymentService.GetPayStubsAsync(employeeId);

                // If no pay stubs are found, return NotFound
                if (payStubs == null || !payStubs.Any())
                {
                    return NotFound(new { message = "No pay stubs found for this employee." });
                }

                // Return the pay stubs
                return Ok(payStubs);
            }
            catch (Exception ex)
            {
                // Handle any general exceptions
                return StatusCode(500, new { message = "An error occurred while retrieving pay stubs.", details = ex.Message });
            }
        }
    }
}

