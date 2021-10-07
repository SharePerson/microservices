namespace Mango.Web.Models
{
    public class CartDetailDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { set; get; }
        public int Count { set; get; }
        public virtual CartHeaderDto CartHeader { set; get; }
        public virtual ProductDto Product { set; get; }
    }
}
