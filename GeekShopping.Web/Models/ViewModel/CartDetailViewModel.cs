namespace GeekShopping.Web.Models.ViewModel
{
    public class CartDetailViewModel
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderViewModel CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int Count { get; set; }
    }
}
