using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class LeaveRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public int? ApprovedById { get; set; }
        public virtual User ApprovedBy { get; set; }
    }
}
