using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponApi.Models
{
    public class Coupon
    {
        [Key]
        public int Id { set; get; }
        public string CouponCode { get; set; }
        public int DiscountAmount { get; set; }
    }
}
