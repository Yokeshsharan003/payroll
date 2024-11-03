namespace EasyPay.DTO
{
    public class LeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public int EmployeeId { get; set; }
    }

}
