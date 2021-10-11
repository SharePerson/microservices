using System.Collections.Generic;

namespace Mango.Web.Models
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { set; get; }

        public CheckoutModel Checkout { set; get; }

        public IEnumerable<CartDetailDto> CartDetails { set; get; }
    }
}
