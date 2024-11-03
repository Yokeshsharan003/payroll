namespace EasyPay.Models
{
    public class OTPData
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int Code { get; set; }

        public DateTime Expiration {  get; set; }

        public bool IsUsed { get; set; }
    }
}
