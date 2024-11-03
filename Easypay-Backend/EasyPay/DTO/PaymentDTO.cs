namespace EasyPay.DTO
{
    public class PaymentDto
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PayDate { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal NetPay { get; set; }
        public List<EarningDto> Earnings { get; set; }
        public List<DeductionDto> Deductions { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
