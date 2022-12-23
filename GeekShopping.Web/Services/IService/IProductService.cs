using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindalAllProducts();
        Task<ProductModel> FindByIdProduct(int id);
        Task<ProductModel> CreateProduct(ProductModel model);
        Task<ProductModel> UpdateProduct(ProductModel model);
        Task<bool> DeleteProductById(int id);
    }
}
