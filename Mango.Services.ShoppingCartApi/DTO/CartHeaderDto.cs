namespace Mango.Services.ShoppingCartApi.DTO
{
    public class CartHeaderDto
    {
        public int Id { get; set; }

        public string UserId { set; get; }

        public string CouponCode { get; set; }

        public double OrderTotal { get; set; }

        public double DiscountTotal { set; get; }
    }
}
