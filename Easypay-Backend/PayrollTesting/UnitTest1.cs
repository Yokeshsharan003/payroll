using EasyPay.Models;
using EasyPay.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using Microsoft.Extensions.Configuration;

namespace PayrollTesting.EasyPay.Tests
{

        [TestFixture]
        public class UnitTest1
        {
            private PayrollContext _context;
            private PayrollService _payrollService;

            [SetUp]
            public void Setup()
            {
            var options = new DbContextOptionsBuilder<PayrollContext>()
  .UseInMemoryDatabase(databaseName: "Zen_Pay")
  .Options;

            
            var mockConfig = new Mock<IConfiguration>();

            
            _context = new PayrollContext(options, mockConfig.Object);
            _payrollService = new PayrollService(_context);

            SeedData(); 
            }

            private void SeedData()
            {
                var employee = new Employee
                {
                    EmployeeId = 1,
                    EmployeeName = "Ram",
                    Designation = "Developer",
                    DateOfJoining = DateTime.Now,
                    Department = "FSD",
                    BasicSalary = 5000,
                    BankName = "SBI",
                    AccountNumber = "45678912345",

                    Grade = new Grade
                    {
                        GradeName = "Grade A", // Required property 'GradeName' must be set
                        PayrollPolicy = new PayrollPolicy
                        {
                            PolicyName = "Standard",
                            Description = "High",
                            EPFPercentage = 12,
                            ProfessionalTax = 200,
                            HealthInsurance = 300,
                            TaxPercentage = 5,
                            HRAAllowancePercentage = 10,
                            BonusPercentage = 5,



                        }
                    },
                    Email = "ram@gmail.com"
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();
            }


            [Test]
            public async Task GeneratePayrollAsync_ValidEmployee_ReturnsPayrollDetailsDto()
            {
                // Arrange
                var employeeId = 1;
                var payDate = DateTime.Now;

                // Act
                var result = await _payrollService.GeneratePayrollAsync(employeeId, payDate);

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(employeeId, Is.EqualTo(result.EmployeeId));
                Assert.That(result.Earnings.First(e => e.Description == "Base Salary").Amount, Is.EqualTo(5000));
                Assert.That(result.Earnings.First(e => e.Description == "House Rent Allowance (HRA)").Amount, Is.EqualTo(5000 * 0.10m));
                Assert.That(result.Deductions.First(e => e.Description == "Health Insurance").Amount, Is.EqualTo(300));

        }
        [Test]
            public async Task GeneratePayrollAsync_InvalidEmployee_ThrowsException()
            {
                // Arrange
                var invalidEmployeeId = 999;
                var payDate = DateTime.Now;

                // Act & Assert
                var ex = Assert.ThrowsAsync<Exception>(async () => await _payrollService.GeneratePayrollAsync(invalidEmployeeId, payDate));
                Assert.That(ex.Message, Is.EqualTo("Employee not found"));
            }

        [Test]
        public async Task GeneratePayrollAsync_ZeroSalaryEmployee_ReturnsZeroEarnings()
        {
            // Arrange
            var employeeId = 3;
            var payDate = DateTime.Now;
            var employee = new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Ram",
                Designation = "Developer",
                DateOfJoining = DateTime.Now,
                Department = "FSD",
                BasicSalary = 0,
                BankName = "SBI",
                AccountNumber = "45678912345",

                Grade = new Grade
                {
                    GradeName = "Grade A", // Required property 'GradeName' must be set
                    PayrollPolicy = new PayrollPolicy
                    {
                        PolicyName = "Standard",
                        Description = "High",
                        EPFPercentage = 12,
                        ProfessionalTax = 200,
                        HealthInsurance = 300,
                        TaxPercentage = 5,
                        HRAAllowancePercentage = 10,
                        BonusPercentage = 5,
                    }
                },
                Email="ram@gmail.com"
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            // Act
            var result = await _payrollService.GeneratePayrollAsync(employeeId, payDate);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Earnings.Sum(e => e.Amount), Is.EqualTo(0));
        }




        [Test]
        public async Task GeneratePayrollAsync_ZeroSalaryEmployee_ReturnsZeroDeductions()
        {
            // Arrange
            var employeeId = 3;
            var payDate = DateTime.Now;
            var employee = new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Ram",
                Designation = "Developer",
                DateOfJoining = DateTime.Now,
                Department = "FSD",
                BasicSalary = 0,
                BankName = "SBI",
                AccountNumber = "45678912345",

                Grade = new Grade
                {
                    GradeName = "Grade A", // Required property 'GradeName' must be set
                    PayrollPolicy = new PayrollPolicy
                    {
                        PolicyName = "Standard",
                        Description = "High",
                        EPFPercentage = 12,
                        ProfessionalTax = 200,
                        HealthInsurance = 300,
                        TaxPercentage = 5,
                        HRAAllowancePercentage = 10,
                        BonusPercentage = 5,
                    }
                },
                Email = "ram@gmail.com"
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            // Act
            var result = await _payrollService.GeneratePayrollAsync(employeeId, payDate);

            // Assert
            Assert.That(result, Is.Not.Null);          
            Assert.That(result.Deductions.Sum(d => d.Amount), Is.EqualTo(0));
        }






        [TearDown]
            public void TearDown()
            {
                _context.Database.EnsureDeleted(); // Clean up after the test
                _context.Dispose();
            }
        }
    }


