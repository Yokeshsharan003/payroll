using casestudy.DAO;
using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal class BenifitsManagement:IBenifitsManagement
    {
        readonly IEasypayRepository _BenifitsManagement;

        public BenifitsManagement()
        {
            _BenifitsManagement = new IEasypayRepositoryImpl();
        }
        public void ManageBenefits()
        {
            Console.WriteLine("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Benefit Type: ");
            string benefitType = Console.ReadLine();

            Console.WriteLine("Enter Benefit Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Effective Date: ");
            DateTime effectiveDate = DateTime.Parse(Console.ReadLine());

            Benefit benefit = new Benefit
            {
                EmployeeID = employeeID,
                BenefitType = benefitType,
                Amount = amount,
                EffectiveDate = effectiveDate
            };

            int result = _BenifitsManagement.ManageBenefits(benefit);

            if (result > 0)
            {
                Console.WriteLine("Benefit added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add benefit.");
            }
        }
        public void UpdateBenefitInformation()
        {
            Console.WriteLine("Enter Benefit ID to update: ");
            int benefitID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Benefit Type: ");
            string benefitType = Console.ReadLine();

            Console.WriteLine("Enter new Benefit Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Effective Date: ");
            DateTime effectiveDate = DateTime.Parse(Console.ReadLine());

            Benefit benefit = new Benefit
            {
                BenefitID = benefitID,
                BenefitType = benefitType,
                Amount = amount,
                EffectiveDate = effectiveDate
            };

            int result = _BenifitsManagement.UpdateBenefitInformation(benefit);

            if (result > 0)
            {
                Console.WriteLine("Benefit updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update benefit.");
            }
        }

    }
}
