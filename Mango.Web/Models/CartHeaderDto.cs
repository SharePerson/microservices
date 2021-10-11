namespace Mango.Web.Models
{
    public class CartHeaderDto
    {
        public int Id { get; set; }

        public string UserId { set; get; }

        public string CouponCode { get; set; }

        public double OrderTotal { get; set; }

        public CouponDto Coupon { set; get; }

        public double DiscountTotal { set; get; }
    }
}
