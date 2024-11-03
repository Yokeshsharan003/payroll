using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using EasyPay.DTO;
using Microsoft.AspNetCore.Authorization;
using EasyPay.Service;
using EasyPay.Exceptions;


namespace EasyPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PayrollContext _context;
        private readonly IEmailService _emailService;

        public UserController(PayrollContext context,IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
               //     Password = u.Password,  // Note: Password should not be exposed in real applications
                    Role = u.Role,
                    EmployeeId = u.EmployeeId
                })
                .ToListAsync();

        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {

         
            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
              //      Password = u.Password,  // Note: Password should not be exposed in real applications
                    Role = u.Role,
                    EmployeeId = u.EmployeeId
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {

            var pass = "";       //s1sap4xz
            for (int i = 0; i <= 7; i++)
            {
                int temp_num = new Random().Next(0, 9);
                if (i == 1 || i == 5)
                {
                    pass += temp_num;
                    continue;
                }
                int temp_alpha = new Random().Next(97, 122);

                pass += (char)temp_alpha;
            }

            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = pass,  // auto generated
                Role = userDto.Role,
                EmployeeId = userDto.EmployeeId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            var userEmail = userDto.Email;
            var messageBody = $"Your login credentials to access ZenPay \n\nUsername : {userEmail}\nPassword : {pass}\nEmployee_Id : {userDto.EmployeeId}";
            try
            {
                _emailService.SendEmailAsync(userEmail, "ZenPay login credentials", messageBody).Wait();
            }
            catch (Exception ex)
            {
                throw new EmailSendingException($"Failed to send email to {userEmail}.", ex);
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);

        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
          //  user.Password = userDto.Password;  // Replace with actual password encryption logic
            user.Role = userDto.Role;
            user.EmployeeId = userDto.EmployeeId;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }

       
    }
}

