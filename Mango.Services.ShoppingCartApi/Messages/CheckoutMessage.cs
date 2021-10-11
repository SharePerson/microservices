using Mango.Services.ShoppingCartApi.DTO;
using System;
using System.Collections.Generic;

namespace Mango.Services.ShoppingCartApi.Messages
{
    public class CheckoutMessage
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public DateTime PickupTime { set; get; }
        public string CardNumber { set; get; }
        public string CVV { set; get; }
        public string MMYY { set; get; }
        public string UserId { set; get; }
        public string CouponCode { set; get; }
        public double DiscountTotal { set; get; }
        public double OrderTotal { set; get; }
        public int CartHeaderId { set; get; }
        public int TotalItems => CartDetails?.Count ?? 0;
        public List<CartDetailDto> CartDetails { set; get; }
    }
}
