using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartApi.Models
{
    public class CardDetail
    {
        [Key]
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { set; get; }
        public int Count { set; get; }

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
    }
}
