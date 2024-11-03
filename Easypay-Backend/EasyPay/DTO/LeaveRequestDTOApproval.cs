namespace EasyPay.DTO
{
    public class LeaveRequestDTOApproval
    {
        public int RequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public int EmployeeId { get; set; }

        public bool IsApproved { get; set; }
    }
}
