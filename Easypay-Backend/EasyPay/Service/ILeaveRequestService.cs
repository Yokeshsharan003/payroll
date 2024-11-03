using EasyPay.DTO;

namespace EasyPay.Service
{
    public interface ILeaveRequestService
    {
        Task<bool> RequestLeaveAsync(LeaveRequestDto leaveRequestDto);
        void ApproveLeaveRequest(int requestId, int approvedByUserId);

        IEnumerable<LeaveRequestDTOApproval> GetAllLeave();

        IEnumerable<LeaveRequestDTOApproval> GetAllLeaveById(int id);
    }

}


