using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web
{
    public static class ApplicationSettings
    {
        public static string ProductApiBase { set; get; }

        public static string ShoppingCartApiBase { set; get; }

        public static string CouponApiBase { set; get; }

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
