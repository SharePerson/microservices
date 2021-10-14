using System;
using System.Collections.Generic;

namespace Mango.Services.OrderApi.Models
{
    public class OrderHeader
    {
        public int Id { set; get; }
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
        public int TotalItems { set; get; }
        public bool PaymentStatus { set; get; }
        public DateTime OrderTime { set; get; }

        public List<OrderDetail> OrderDetails { set; get; }
    }
}
