using EasyPay.DTO;
using EasyPay.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly PayrollContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentService(PayrollContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task ProcessPaymentAsync(int employeeId, decimal amount, DateTime paymentDate, string paymentMethod)
        {
            // Check if the employee exists
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            // Create a new payment record
            var payment = new Payment
            {
                EmployeeId = employeeId,
                Amount = amount,
                PaymentDate = paymentDate,
                PaymentMethod = paymentMethod
            };

            // Add the payment record to the database
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<PaymentDto>> GetPayStubsAsync(int employeeId)
        {

            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims
          .FirstOrDefault(c => c.Type == "EmployeeId");

            if (employeeId == Convert.ToInt32(userIdClaim.Value))
            {

                return await _context.Payrolls
                .Where(p => p.EmployeeId == employeeId) 
                .Include(p => p.Employee) 
                .Include(p => p.Earnings) 
                .Include(p => p.Deductions) 
                .Select(p => new PaymentDto
                {
                    PayrollId = p.PayrollId,
                    EmployeeId = p.EmployeeId,
                    PayDate = p.PayDate,
                    GrossSalary = p.TotalEarnings,
                    DeductionAmount = p.TotalDeductions,
                    NetPay = p.NetAmount,
                    Earnings = p.Earnings.Select(e => new EarningDto
                    {
                        EarningId = e.EarningId,
                        Description = e.Description,
                        Amount = e.Amount
                    }).ToList(),
                    Deductions = p.Deductions.Select(d => new DeductionDto
                    {
                        DeductionId = d.DeductionId,
                        Description = d.Description,
                        Amount = d.Amount
                    }).ToList(),
                    Employee = p.Employee != null ? new EmployeeDto
                    {
                        EmployeeId = p.Employee.EmployeeId,
                        EmployeeName = p.Employee.EmployeeName,
                        Designation = p.Employee.Designation,
                        DateOfJoining = p.Employee.DateOfJoining,
                        Department = p.Employee.Department,
                        BasicSalary = p.Employee.BasicSalary,
                        BankName = p.Employee.BankName,
                        AccountNumber = p.Employee.AccountNumber,
                        GradeId = p.Employee.GradeId
                    } : null
                })
                .ToListAsync();
            }
            else
            {
                return null;
            }
        }


    }
}

