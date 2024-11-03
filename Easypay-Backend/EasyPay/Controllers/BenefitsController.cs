using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using EasyPay.DTO;
using EasyPay.Service;
using Microsoft.AspNetCore.Authorization;

namespace EasyPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenefitsController : ControllerBase
    {
        private readonly IBenefitService _benefitService;

        public BenefitsController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }

        [HttpGet]
        [Authorize(Roles = "PayrollProcessor")]
        public IActionResult GetAllBenefits()
        {
            var benefits = _benefitService.GetAllBenefits();
            return Ok(benefits);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "PayrollProcessor")]
        public IActionResult GetBenefit(int id)
        {
            var benefit = _benefitService.GetBenefitById(id);
            if (benefit == null) return NotFound();
            return Ok(benefit);
        }

        [HttpPost]
        [Authorize(Roles = "PayrollProcessor")]
        public async Task<IActionResult> AddBenefit([FromBody] BenefitRequestDto benefitDto)
        {
            try
            {
                var result = await _benefitService.AddBenefitAsync(benefitDto); // Await the result here
                return Ok(result);  // This will now return the string "Benefit added successfully."
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        [Authorize(Roles = "PayrollProcessor")]
        public async Task<IActionResult> UpdateBenefit(int id, [FromBody] BenefitRequestDto benefitDto)
        {
            try
            {
                if (id <= 0) return BadRequest();
                _benefitService.UpdateBenefitAsync(id, benefitDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "PayrollProcessor")]
        public IActionResult DeleteBenefit(int id)
        {
            _benefitService.DeleteBenefit(id);
            return Ok();
        }
    }

}
