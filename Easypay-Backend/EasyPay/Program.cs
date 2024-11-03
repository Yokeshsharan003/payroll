using EasyPay.Models;
using EasyPay.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;  // For FileInfo

namespace EasyPay
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));  // Initialize log4net logger

        public static void Main(string[] args)
        {
            // Configure log4net
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Application is starting...");  // Log application start

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<PayrollContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PayrollConStr")));

            builder.Services.AddControllers();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", corsBuilder =>
                {
                    corsBuilder.WithOrigins("http://localhost:3000") // React app URL
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                });
            });

            builder.Services.AddHttpContextAccessor();

            // JWT Authentication configuration
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role, // Ensure that roles are validated
                };
            });

            // Authorization policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
                options.AddPolicy("PayrollProcessorPolicy", policy => policy.RequireRole("PayrollProcessor"));
                options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
            });

            // Adding scoped services
            builder.Services.AddScoped<IPayrollService, PayrollService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            builder.Services.AddScoped<IBenefitService, BenefitService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Add configuration from appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Add Swagger documentation
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ZenPay", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Swagger for development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Routing middleware
            app.UseRouting();

            // Apply CORS policy
            app.UseCors("AllowReactApp");

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map Controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Fallback for SPA (React)
            app.MapFallbackToFile("/index.html");

            log.Info("Application is running...");  // Log application running status

            app.Run();
        }
    }
}
