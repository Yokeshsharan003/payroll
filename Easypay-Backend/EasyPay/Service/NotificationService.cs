using EasyPay.Models;

namespace EasyPay.Service
{
    public class NotificationService
    {
        private readonly PayrollContext _context;

        public NotificationService(PayrollContext context)
        {
            _context = context;
        }

        public void AddNotification(int userId, string message, string notificationType)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                NotificationType = notificationType,  // Set notification type
                CreatedDate = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public IEnumerable<Notification> GetUserNotifications(int userId)
        {
            return _context.Notifications
                           .Where(n => n.UserId == userId && !n.IsRead)
                           .OrderByDescending(n => n.CreatedDate)
                           .ToList();
        }

        public void MarkAsRead(int notificationId)
        {
            var notification = _context.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                _context.SaveChanges();
            }
        }

        // Method to handle leave request submitted notification
        public void NotifyLeaveRequestSubmitted(int employeeId, string message)
        {
            var managerId = GetManagerIdForEmployee(); // Fetch manager's ID from User table
            if (managerId.HasValue)
            {
                AddNotification(managerId.Value, message, "LeaveRequestSubmitted");
            }
        }

        // Method to handle leave request approved notification
        public void NotifyLeaveRequestApproved(int userId, string message)
        {
            AddNotification(userId, message, "LeaveRequestApproved");
        }

        private int? GetManagerIdForEmployee()
        {
            // Implement logic to get manager ID
            // Example logic: Fetch a user with the role 'Manager'
            var manager = _context.Users
                                  .FirstOrDefault(u => u.Role == "Manager");

            // Return manager ID if found, otherwise null
            return manager?.UserId;
        }
    }




}
