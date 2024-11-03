using EasyPay.DTO;
using EasyPay.Models;

namespace EasyPay.Service
{
    public interface IBenefitService
    {
        IEnumerable<BenefitResponseDto> GetAllBenefits();

        BenefitResponseDto GetBenefitById(int benefitId);

        Task<string> AddBenefitAsync(BenefitRequestDto benefitDto);

        Task<string> UpdateBenefitAsync(int id, BenefitRequestDto benefitDto);

        void DeleteBenefit(int benefitId);
    }


}
