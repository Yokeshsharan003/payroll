
using EasyPay.DTO;
using EasyPay.Models;
using Microsoft.EntityFrameworkCore;
using log4net;


namespace EasyPay.Service
{
    public class PayrollService : IPayrollService
    {
        private readonly PayrollContext _context;
        private static readonly ILog log = LogManager.GetLogger(typeof(PayrollService));

        public PayrollService(PayrollContext context)
        {
            _context = context;
        }

        public async Task<PayrollDetailsDto> GeneratePayrollAsync(int employeeId, DateTime payDate)
        {

            log.Info("Starting payroll calculation...");
            // Extract year and month from the payDate
            var currentYear = payDate.Year;
            var currentMonth = payDate.Month;

            // Check if payroll has already been generated in the same month and year for the employee
            var existingPayroll = await _context.Payrolls
                .FirstOrDefaultAsync(p => p.EmployeeId == employeeId &&
                                          p.PayDate.Year == currentYear &&
                                          p.PayDate.Month == currentMonth);

            if (existingPayroll != null)
            {
                // Return a message indicating payroll already exists for the month
                throw new Exception($"Payroll has already been generated for Employee ID {employeeId} in {payDate.ToString("MMMM yyyy")}. Payroll can only be generated once per month.");
            }

            // Fetch employee and related payroll policy
            var employee = await _context.Employees
                .Include(e => e.Grade)
                .ThenInclude(g => g.PayrollPolicy)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            var payrollPolicy = employee.Grade.PayrollPolicy;
            var baseSalary = employee.BasicSalary;

            // Calculate earnings
            var hra = (baseSalary * payrollPolicy.HRAAllowancePercentage) / 100;
            var bonus = (baseSalary * payrollPolicy.BonusPercentage) / 100;

            // Create earnings list
            var earnings = new List<Earning>
    {
        new Earning { EmployeeId = employeeId, Amount = baseSalary, Description = "Base Salary" },
        new Earning { EmployeeId = employeeId, Amount = hra, Description = "House Rent Allowance (HRA)" },
        new Earning { EmployeeId = employeeId, Amount = bonus, Description = "Performance Bonus" }
    };

            // Calculate Gross Salary, Total Earnings
            var grossSalary = earnings.Sum(e => e.Amount);
            var totalEarnings = earnings.Sum(e => e.Amount);

            // Calculate deductions
            var epf = (baseSalary * payrollPolicy.EPFPercentage) / 100;
            var professionalTax = payrollPolicy.ProfessionalTax;
            var healthInsurance = payrollPolicy.HealthInsurance;
            var incomeTax = (grossSalary - (epf + healthInsurance)) * payrollPolicy.TaxPercentage / 100;

            // Create deductions list
            var deductions = new List<Deduction>
    {
        new Deduction { EmployeeId = employeeId, Amount = epf, Description = "Employee Provident Fund (EPF)" },
        new Deduction { EmployeeId = employeeId, Amount = professionalTax, Description = "Professional Tax" },
        new Deduction { EmployeeId = employeeId, Amount = healthInsurance, Description = "Health Insurance" },
        new Deduction { EmployeeId = employeeId, Amount = incomeTax, Description = "Income Tax" }
    };

            // Calculate Total Deductions
            var totalDeductions = deductions.Sum(d => d.Amount);
            var net = totalEarnings - totalDeductions;

            if (baseSalary > 0)
            {
                // Create Payroll object
                var payroll = new Payroll
                {
                    EmployeeId = employeeId,
                    PayDate = payDate,
                    Earnings = earnings,
                    Deductions = deductions,
                    TotalDeductions = totalDeductions,
                    TotalEarnings = totalEarnings,
                    NetAmount = net
                };

                // Save to the database
                _context.Payrolls.Add(payroll);
                await _context.SaveChangesAsync();

                // Return Payroll details as DTO
                return new PayrollDetailsDto
                {
                    PayrollId = payroll.PayrollId,
                    EmployeeId = payroll.EmployeeId,
                    PayDate = payroll.PayDate,
                    Earnings = payroll.Earnings.Select(e => new EarningDto
                    {
                        EarningId = e.EarningId,
                        Amount = e.Amount,
                        Description = e.Description
                    }).ToList(),
                    Deductions = payroll.Deductions.Select(d => new DeductionDto
                    {
                        DeductionId = d.DeductionId,
                        Amount = d.Amount,
                        Description = d.Description
                    }).ToList(),
                    GrossSalary = grossSalary, // Calculated value
                    TotalDeductions = totalDeductions, // Calculated value
                    TotalEarnings = totalEarnings, // New field
                    NetPay = payroll.NetAmount // Calculated value
                };
            }
            else
            {
                // Create Payroll object
                var payroll = new Payroll
                {
                    EmployeeId = employeeId,
                    PayDate = payDate,
                    Earnings = earnings,
                    Deductions = deductions,
                    TotalDeductions = totalDeductions,
                    TotalEarnings = totalEarnings,
                    NetAmount = net
                };

          
                // Return Payroll details as DTO
                return new PayrollDetailsDto
                {
                    PayrollId = payroll.PayrollId,
                    EmployeeId = payroll.EmployeeId,
                    PayDate = payroll.PayDate,
                    Earnings = payroll.Earnings.Select(e => new EarningDto
                    {
                        EarningId = e.EarningId,
                        Amount = 0,
                        Description = e.Description
                    }).ToList(),
                    Deductions = payroll.Deductions.Select(d => new DeductionDto
                    {
                        DeductionId = d.DeductionId,
                        Amount = 0,
                        Description = d.Description
                    }).ToList(),
                    GrossSalary = grossSalary, // Calculated value
                    TotalDeductions = totalDeductions, // Calculated value
                    TotalEarnings = totalEarnings, // New field
                    NetPay = payroll.NetAmount // Calculated value
                };
            }
        }




        public decimal CalculateNetPay(int payrollId)
        {
            var payroll = _context.Payrolls
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .FirstOrDefault(p => p.PayrollId == payrollId);

            if (payroll == null)
                throw new Exception("Payroll not found");

            return payroll.TotalEarnings - payroll.TotalDeductions; // Calculate NetPay
        }

        public bool VerifyPayrollData(int payrollId)
        {
            var payroll = _context.Payrolls
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .FirstOrDefault(p => p.PayrollId == payrollId);

            if (payroll == null)
                return false;

            var calculatedTotalDeductions = payroll.Deductions.Sum(d => d.Amount); // Calculate TotalDeductions
            var calculatedNetPay = payroll.TotalEarnings - calculatedTotalDeductions; // Calculate NetPay

            return payroll.NetAmount == calculatedNetPay;
        }

        public async Task<IEnumerable<PayrollDto>> ReviewDepartmentPayrollsAsync(string department)
        {
            var employees = await _context.Employees
                .Where(e => e.Department == department)
                .Select(e => e.EmployeeId)
                .ToListAsync();

            var payrolls = await _context.Payrolls
                .Where(p => employees.Contains(p.EmployeeId))
                .Include(p => p.Earnings)
                .Include(p => p.Deductions)
                .ToListAsync();

            return payrolls.Select(p => new PayrollDto
            {
                PayrollId = p.PayrollId,
                EmployeeId = p.EmployeeId,
                PayDate = p.PayDate,
                Earnings = p.Earnings.Select(e => new EarningDto
                {
                    EarningId = e.EarningId,
                    Amount = e.Amount,
                    Description = e.Description
                }).ToList(),
                Deductions = p.Deductions.Select(d => new DeductionDto
                {
                    DeductionId = d.DeductionId,
                    Amount = d.Amount,
                    Description = d.Description
                }).ToList(),
                GrossSalary = p.TotalEarnings, // Calculated value
                TotalDeductions = p.TotalDeductions, // Calculated value
                TotalEarnings = p.Earnings.Sum(e => e.Amount), // New calculation
                NetPay = p.NetAmount // Calculated value
            });
        }

    }
}






