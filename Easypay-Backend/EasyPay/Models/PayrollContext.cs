namespace EasyPay.Models
{
    using Microsoft.EntityFrameworkCore;

    public class PayrollContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayrollPolicy> PayrollPolicies { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ComplianceReport> ComplianceReports { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Earning> Earnings { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<OTPData> OTPData { get; set; }


        public PayrollContext(DbContextOptions<PayrollContext> options, IConfiguration config)
        : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("PayrollConStr"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            


            // Employee entity
            modelBuilder.Entity<Employee>()
                .HasKey(emp => emp.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Grade)
                .WithMany(g => g.Employees)
                .HasForeignKey(emp => emp.GradeId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<User>()
            //    .HasMany(emp => emp.LeaveRequests)
            //    .WithOne(lr => lr.User)
            //    .HasForeignKey(lr => lr.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
        .Property(e => e.BasicSalary)
        .HasColumnType("decimal(18,2)");

           

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PayrollPolicy>()
                .Property(pp => pp.BonusPercentage)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<PayrollPolicy>()
                .Property(pp => pp.HRAAllowancePercentage)
                .HasColumnType("decimal(5,2)");

            // Configure other entities
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>()
               .HasMany(e => e.Payments)
               .WithOne(p => p.Employee)
               .HasForeignKey(p => p.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<PayrollPolicy>()
                .HasKey(pp => pp.PayrollPolicyId);




            modelBuilder.Entity<Benefit>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");

            // Payroll entity
            modelBuilder.Entity<Payroll>()
                .HasKey(p => p.PayrollId);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payroll>()
                .HasMany(p => p.Earnings)
                .WithOne(e => e.Payroll)
                .HasForeignKey(e => e.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payroll>()
                .HasMany(p => p.Deductions)
                .WithOne(d => d.Payroll)
                .HasForeignKey(d => d.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payroll>()
            .Property(p => p.TotalDeductions)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payroll>()
            .Property(p => p.TotalEarnings)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payroll>()
            .Property(p => p.NetAmount)
            .HasColumnType("decimal(18,2)");

            // Earning entity
            modelBuilder.Entity<Earning>()
                .HasKey(e => e.EarningId);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.Employee)
                .WithMany(emp => emp.Earning)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.Payroll)
                .WithMany(p => p.Earnings)
                .HasForeignKey(e => e.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            // Deduction entity
            modelBuilder.Entity<Deduction>()
                .HasKey(d => d.DeductionId);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Employee)
                .WithMany(emp => emp.Deduction)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Payroll)
                .WithMany(p => p.Deductions)
                .HasForeignKey(d => d.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            

            // Grade entity
            modelBuilder.Entity<Grade>()
                .HasKey(g => g.GradeId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.PayrollPolicy)
                .WithMany()
                .HasForeignKey(g => g.PayrollPolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            // PayrollPolicy entity
            modelBuilder.Entity<PayrollPolicy>()
                .HasKey(pp => pp.PayrollPolicyId);

            // LeaveRequest entity
            modelBuilder.Entity<LeaveRequest>()
                .HasKey(lr => lr.RequestId);

            //modelBuilder.Entity<LeaveRequest>()
            //    .HasOne(lr => lr.User)
            //    .WithMany(emp => emp.LeaveRequests)
            //    .HasForeignKey(lr => lr.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.ApprovedBy)
                .WithMany()
                .HasForeignKey(lr => lr.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification entity
            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne()
                .HasForeignKey<User>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);



            //store type
            modelBuilder.Entity<Employee>()
        .Property(e => e.BasicSalary)
        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Earning>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Deduction>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Benefit>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PayrollPolicy>()
                .Property(p => p.EPFPercentage)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<PayrollPolicy>()
                .Property(p => p.HealthInsurance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PayrollPolicy>()
                .Property(p => p.ProfessionalTax)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PayrollPolicy>()
                .Property(p => p.TaxPercentage)
                .HasColumnType("decimal(18,2)");

            // Configure other entities
            modelBuilder.Entity<Payroll>()
                .HasKey(p => p.PayrollId);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payroll>()
                .HasMany(p => p.Earnings)
                .WithOne(e => e.Payroll)
                .HasForeignKey(e => e.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payroll>()
                .HasMany(p => p.Deductions)
                .WithOne(d => d.Payroll)
                .HasForeignKey(d => d.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Earning>()
                .HasKey(e => e.EarningId);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.Employee)
                .WithMany(emp => emp.Earning)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.Payroll)
                .WithMany(p => p.Earnings)
                .HasForeignKey(e => e.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deduction>()
                .HasKey(d => d.DeductionId);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Employee)
                .WithMany(emp => emp.Deduction)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Payroll)
                .WithMany(p => p.Deductions)
                .HasForeignKey(d => d.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasKey(emp => emp.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Grade)
                .WithMany(g => g.Employees)
                .HasForeignKey(emp => emp.GradeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.GradeId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.PayrollPolicy)
                .WithMany()
                .HasForeignKey(g => g.PayrollPolicyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PayrollPolicy>()
                .HasKey(pp => pp.PayrollPolicyId);

            modelBuilder.Entity<LeaveRequest>()
                .HasKey(lr => lr.RequestId);

            //modelBuilder.Entity<LeaveRequest>()
            //    .HasOne(lr => lr.User)
            //    .WithMany(emp => emp.LeaveRequests)
            //    .HasForeignKey(lr => lr.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.ApprovedBy)
                .WithMany()
                .HasForeignKey(lr => lr.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne()
                .HasForeignKey<User>(u => u.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payroll>()
    .HasKey(p => p.PayrollId);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Payroll)
                .WithMany(p => p.Deductions)
                .HasForeignKey(d => d.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

    }

}



