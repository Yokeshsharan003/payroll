using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyPay.Models;
using EasyPay.Service;

namespace EasyPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserNotifications(int userId)
        {
            var notifications = _notificationService.GetUserNotifications(userId);
            return Ok(notifications);
        }

        [HttpPost("mark-as-read/{notificationId}")]
        public IActionResult MarkNotificationAsRead(int notificationId)
        {
            _notificationService.MarkAsRead(notificationId);
            return Ok("Notification marked as read.");
        }
    }

}
