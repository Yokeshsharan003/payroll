using EasyPay.DTO;
using EasyPay.Exceptions;
using EasyPay.Models;
using EasyPay.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly PayrollContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;

    public LeaveRequestService(PayrollContext context, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
    }


    // Method for requesting leave
    public async Task<bool> RequestLeaveAsync(LeaveRequestDto leaveRequestDto)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
           .FirstOrDefault(c => c.Type == "EmployeeId");

        if (leaveRequestDto.EmployeeId == Convert.ToInt32(userIdClaim.Value))
        {
            var leaveRequest = new LeaveRequest
            {
                StartDate = leaveRequestDto.StartDate,
                EndDate = leaveRequestDto.EndDate,
                Reason = leaveRequestDto.Reason,
                IsApproved = false,
                EmployeeId = leaveRequestDto.EmployeeId
            };

            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();

            // Send email to manager
            var managerEmail = await _context.Users
                                .Where(u => u.Role == "Manager")
                                .Select(u => u.Email)
                                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(managerEmail))
            {
                var messageBody = $"Employee {leaveRequestDto.EmployeeId} has requested leave from {leaveRequestDto.StartDate.ToShortDateString()} to {leaveRequestDto.EndDate.ToShortDateString()}.";
                try
                {
                    await _emailService.SendEmailAsync(managerEmail, "New Leave Request", messageBody);
                }
                catch (Exception ex)
                {
                    throw new EmailSendingException($"Failed to send email to {managerEmail}.", ex);

                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }





    public void ApproveLeaveRequest(int requestId, int approvedByUserId)
    {
        try
        {
            var leaveRequest = _context.LeaveRequests
                                       .Include(lr => lr.Employee)
                                       .FirstOrDefault(lr => lr.RequestId == requestId);

          
            if (leaveRequest != null)
            {
                var approvingUser = _context.Users
                                            .FirstOrDefault(u => u.UserId == approvedByUserId && u.Role == "Manager");

                if (approvingUser == null)
                {
                    throw new Exception("Only managers can approve leave requests");
                }

                leaveRequest.IsApproved = true;
                leaveRequest.ApprovedById = approvedByUserId;
                _context.SaveChanges();

                // Send email to employee after approval
               
                    var employeeEmail = leaveRequest.Employee.Email;
                    var messageBody = $"Your leave request from {leaveRequest.StartDate.ToShortDateString()} to {leaveRequest.EndDate.ToShortDateString()} has been approved by ManagerID : {approvedByUserId}";
                try
                {
                    _emailService.SendEmailAsync(employeeEmail, "Leave Request Approved", messageBody).Wait();
                }
                catch (Exception ex)
                {
                    throw new EmailSendingException($"Failed to send email to {employeeEmail}.", ex);
                }
            }
            else
            {
                throw new Exception("Leave request not found");
            }
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException?.Message;
            throw new Exception($"An error occurred while saving the entity changes: {innerException}");
        }
    }
    public IEnumerable<LeaveRequestDTOApproval> GetAllLeave()
    {
        return _context.LeaveRequests.Include(b => b.Employee)
                                     .Where(b=>!b.IsApproved)
                                     .Select(b => new LeaveRequestDTOApproval
                                {
                                    RequestId = b.RequestId,
                                    StartDate = b.StartDate,
                                    EndDate = b.EndDate,
                                    Reason = b.Reason,
                                    EmployeeId = b.EmployeeId,
                                    IsApproved = b.IsApproved
                                }).ToList();
    }

    public IEnumerable<LeaveRequestDTOApproval> GetAllLeaveById(int id)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "EmployeeId");

        return _context.LeaveRequests
            .Where(b => b.EmployeeId == id && id == Convert.ToInt32(userIdClaim.Value))
            .Select(b => new LeaveRequestDTOApproval
            {
                RequestId = b.RequestId,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Reason = b.Reason,
                EmployeeId = b.EmployeeId,
                IsApproved = b.IsApproved
            })
            .ToList();
    }





}





















//// Method for approving a leave request

//public async Task<bool> ApproveLeaveRequestAsync(int requestId)
//{
//    var leaveRequest = await _context.LeaveRequests
//        .FirstOrDefaultAsync(lr => lr.RequestId == requestId);

//    if (leaveRequest == null)
//        return false; // Request not found

//    // Extract the manager's user ID from the JWT token claims
//    var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
//        .FirstOrDefault(c => c.Type == "UserId");

//    if (userIdClaim == null)
//        return false; // UserId claim not found

//    var userId = int.Parse(userIdClaim.Value);

//    // Mark the leave request as approved and set the ApprovedById
//    leaveRequest.IsApproved = true;
//    leaveRequest.ApprovedById = userId;

//    _context.LeaveRequests.Update(leaveRequest);
//    await _context.SaveChangesAsync();

//    return true;
//}
