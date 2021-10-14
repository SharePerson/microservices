using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderApi.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public int Count { set; get; }
        public string ProductName { set; get; }
        public double Price { get; set; }
        public int ProductId { set; get; }

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader OrderHeader { set; get; }
    }
}
