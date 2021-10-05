using System.Collections.Generic;

namespace Mango.Services.ShoppingCartApi.Models
{
    public class Cart
    {
        public CartHeader CartHeader { set; get; }

        public IEnumerable<CardDetail> CardDetails { set; get; }
    }
}
