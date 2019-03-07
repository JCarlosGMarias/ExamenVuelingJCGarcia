
namespace IbmServices.Models
{
    public class Transaction : BaseEntity
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
