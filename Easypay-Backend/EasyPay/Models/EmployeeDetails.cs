using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int EmployeeDetailsId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string BloodGroup { get; set; }

       // public byte[]? Photo { get; set; } // Store image as byte array

        // Navigation Property
        public virtual Employee Employee { get; set; }
    }

}
