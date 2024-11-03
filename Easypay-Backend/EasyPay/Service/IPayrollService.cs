using EasyPay.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EasyPay.Service
{
    
    public interface IPayrollService
    {
        Task<PayrollDetailsDto> GeneratePayrollAsync(int employeeId, DateTime payDate);
        decimal CalculateNetPay(int payrollId);
        bool VerifyPayrollData(int payrollId);
        Task<IEnumerable<PayrollDto>> ReviewDepartmentPayrollsAsync(string department);
    }


}
