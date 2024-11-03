using EasyPay.DTO;
using EasyPay.Models;

namespace EasyPay.Service
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(int employeeId, decimal amount, DateTime paymentDate, string paymentMethod);
        Task<IEnumerable<PaymentDto>> GetPayStubsAsync(int employeeId);
    }
}
