using System;

namespace Mango.Web.Models
{
    public class CheckoutModel
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public DateTime PickupTime { set; get; }
        public string CardNumber { set; get; }
        public string CVV { set; get; }
        public string MMYY { set; get; }
     }
}
