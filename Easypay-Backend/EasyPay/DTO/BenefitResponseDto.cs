namespace EasyPay.DTO
{
    public class BenefitResponseDto
    {
        public int BenefitId { get; set; }
        public string BenefitName { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }  // Example of including related data
    }
}
