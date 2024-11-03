using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class ComplianceReport
    {
        [Key]
        public int CRId { get; set; }

        [Required]
        [StringLength(100)]
        
        public string ReportName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }

}
