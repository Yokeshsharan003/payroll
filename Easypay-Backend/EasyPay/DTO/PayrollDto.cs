namespace EasyPay.DTO
{
    public class PayrollDto
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PayDate { get; set; }
        public List<EarningDto> Earnings { get; set; }
        public List<DeductionDto> Deductions { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalEarnings { get; set; } // New field
        public decimal NetPay { get; set; }
    }
}
