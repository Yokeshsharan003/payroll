using EasyPay.DTO;
using EasyPay.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Service
{
    public class BenefitService : IBenefitService
    {
        private readonly PayrollContext _context;

        public BenefitService(PayrollContext context)
        {
            _context = context;
        }

        public IEnumerable<BenefitResponseDto> GetAllBenefits()
        {
            return _context.Benefits.Include(b => b.Employee)
                                    .Select(b => new BenefitResponseDto
                                    {
                                        BenefitId = b.BenefitId,
                                        BenefitName = b.BenefitName,
                                        Amount = b.Amount,
                                        EmployeeId = b.EmployeeId,
                                        EmployeeName = b.Employee.EmployeeName
                                    }).ToList();
        }

        public BenefitResponseDto GetBenefitById(int benefitId)
        {
            var benefit = _context.Benefits.Include(b => b.Employee)
                             .FirstOrDefault(b => b.BenefitId == benefitId);

            if (benefit == null) return null;

            return new BenefitResponseDto
            {
                BenefitId = benefit.BenefitId,
                BenefitName = benefit.BenefitName,
                Amount = benefit.Amount,
                EmployeeName = benefit.Employee.EmployeeName
            };
        }

        public async Task<string> AddBenefitAsync(BenefitRequestDto benefitDto)
        {
            var employee = await _context.Employees
                .Include(e => e.Grade)
                .ThenInclude(g => g.PayrollPolicy)
                .FirstOrDefaultAsync(e => e.EmployeeId == benefitDto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            var payrollPolicy = employee.Grade.PayrollPolicy;
            var baseSalary = employee.BasicSalary;
            var epf = (baseSalary * payrollPolicy.EPFPercentage) / 100;

            var benefit = new Benefit
            {
                BenefitName = benefitDto.BenefitName,
                Amount = 2*epf,
                EmployeeId = benefitDto.EmployeeId
            };

            _context.Benefits.Add(benefit);
            await _context.SaveChangesAsync();

            return "Benefit added successfully.";
        }

        public async Task<string> UpdateBenefitAsync(int id, BenefitRequestDto benefitDto)
        {
            var employee = await _context.Employees
                .Include(e => e.Grade)
                .ThenInclude(g => g.PayrollPolicy)
                .FirstOrDefaultAsync(e => e.EmployeeId == benefitDto.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            var payrollPolicy = employee.Grade.PayrollPolicy;
            var baseSalary = employee.BasicSalary;
            var epf = (baseSalary * payrollPolicy.EPFPercentage) / 100;

            var benefit = new Benefit
            {
                BenefitName = benefitDto.BenefitName,
                Amount = 2 * epf,
                EmployeeId = benefitDto.EmployeeId
            };

            _context.Benefits.Update(benefit);
            await _context.SaveChangesAsync();

            return "Benefit updated successfully.";
        }

        public void DeleteBenefit(int benefitId)
        {
            var benefit = _context.Benefits.Find(benefitId);
            if (benefit != null)
            {
                _context.Benefits.Remove(benefit);
                _context.SaveChanges();
            }
        }
    }
}
