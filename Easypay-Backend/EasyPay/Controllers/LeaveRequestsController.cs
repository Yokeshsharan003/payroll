using global::EasyPay.DTO;
using global::EasyPay.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EasyPay.Controllers
{

    namespace EasyPay.Controllers
    {

        [Route("api/[controller]")]
        public class LeaveRequestController : ControllerBase
        {
            private readonly ILeaveRequestService _leaveRequestService;

            public LeaveRequestController(ILeaveRequestService leaveRequestService)
            {
                _leaveRequestService = leaveRequestService;
            }

            // POST: api/LeaveRequest/RequestLeave
            // POST: api/LeaveRequest/RequestLeave
            [HttpPost("RequestLeave")]
            [Authorize(Roles = "Employee,Admin,PayrollProcessor")]
            public async Task<IActionResult> RequestLeave([FromBody] LeaveRequestDto leaveRequestDto)
            {
                if (leaveRequestDto == null)
                    return BadRequest("Leave request data is required.");

                var result = await _leaveRequestService.RequestLeaveAsync(leaveRequestDto);

                if (result)
                    return Ok(new { Message = "Leave request submitted successfully." });
                else
                    return BadRequest(new { Message = "Failed to submit leave request." });
            }

            // POST: api/LeaveRequest/ApproveLeaveRequest
            [Authorize(Roles = "Manager")]
            [HttpPost("{requestId:int}/approve")]
            public IActionResult ApproveLeaveRequest(int requestId)
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == "EmployeeId").Value);
                _leaveRequestService.ApproveLeaveRequest(requestId, userId);
                return Ok("Leave request approved.");
            }

            [HttpGet]
            [Authorize(Roles = "Manager")]
            public IActionResult GetAllLeaveRequest()
            {
                var leave = _leaveRequestService.GetAllLeave();
                return Ok(leave);
            }

            [HttpGet("Leave_Status{id}")]
            [Authorize(Roles = "PayrollProcessor,Employee,Admin")]
            public IActionResult GetLeaveStatus(int id)
            {
                var leave = _leaveRequestService.GetAllLeaveById(id);
                if (leave == null) return NotFound();
                return Ok(leave);
            }

        }


    }
}
