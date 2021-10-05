using System.ComponentModel.DataAnnotations;


namespace Mango.Services.ShoppingCartApi.Models
{
    public class CartHeader
    {
        [Key]
        public int Id { get; set; }

        public string UserId { set; get; }

        public string CouponCode { get; set; }
    }
}
