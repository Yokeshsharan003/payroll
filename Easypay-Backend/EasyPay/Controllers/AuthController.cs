using EasyPay.DTO;
using EasyPay.Exceptions;
using EasyPay.Models;
using EasyPay.Service;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using log4net;

namespace EasyPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration _config;
        private readonly PayrollContext _context;
        private readonly IEmailService _emailService;
        private static readonly ILog log = LogManager.GetLogger(typeof(AuthController));

        public AuthController(IConfiguration configuration, PayrollContext context, IEmailService emailService)    //DBCONTEXT
        {
            this._config = configuration;
            _context = context;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] AuthDto user)
        {
            IActionResult response = Unauthorized();
            if (user != null)
            {
                var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (dbUser != null && user.Password.Equals(dbUser.Password))
                {
                    // Token Generation
                    var issuer = _config["Jwt:Issuer"];
                    var audience = _config["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                    var signingCredentials = new SigningCredentials(
                                                new SymmetricSecurityKey(key),
                                                SecurityAlgorithms.HmacSha512Signature);

                    // Adding claims including role
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, dbUser.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, dbUser.Email),
                        new Claim(ClaimTypes.Role, dbUser.Role),// Correct role claim
                        new Claim ("EmployeeId",dbUser.EmployeeId.ToString())

                    };

                    var expires = DateTime.UtcNow.AddMinutes(10);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = expires,
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = signingCredentials
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    log.Info($"User with email {user.Email} has logged in at {DateTime.Now}");
                    return Ok(jwtToken);
                }
            }
            return response;
        }

        
        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetAuthAsync([FromBody] ForgetPasswordDTO user)
        {

            var code = new Random().Next(100000, 999999);

            IActionResult response = NotFound();
            if (user != null)
            {
                var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (dbUser != null)
                {
                    var OTPDatas = new OTPData
                    {
                        Email = user.Email,
                        Code = code,
                        Expiration = DateTime.UtcNow.AddMinutes(3), // Code valid for 3 minutes
                       
                    };

                    _context.OTPData.Add(OTPDatas);

                    await _context.SaveChangesAsync();

                    var userEmail = user.Email;
                    var messageBody = $"Enter this code to change password : {code}";
                    try
                    {
                        _emailService.SendEmailAsync(userEmail, "ZenPay change password", messageBody).Wait();
                    }
                    catch (Exception ex)
                    {
                        throw new EmailSendingException($"Failed to send email to {userEmail}.", ex);
                    }
                   return Ok("Verification code sent to your email.");
                }
               
            }
            return response;

        }


        [AllowAnonymous]
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody] CodeCheckDTO request)
        {
            var email = request.Email;
            var code = request.Code;

            
            var verificationEntry = await _context.OTPData
                .FirstOrDefaultAsync(v => v.Email == email && v.Code == code && v.Expiration > DateTime.UtcNow && !v.IsUsed);

            if (verificationEntry == null)
            {
                return BadRequest("Invalid or expired verification code.");
            }

            verificationEntry.IsUsed = true;
            await _context.SaveChangesAsync();

            
            return Ok(true);
        }



        [AllowAnonymous]
        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> PassChangeAuth([FromBody] ChangePasswordDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (user != null)
            {
                var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (dbUser != null)
                {
                   dbUser.Password = user.Password;

                    try
                    {
                        await _context.SaveChangesAsync();
                        return Ok(true);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error updating password.");
                    }

                }

            }
            return NotFound();
           

        }


    }
}
