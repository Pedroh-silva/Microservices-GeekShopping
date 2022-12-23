using AutoMapper;
using GeekShopping.ProductAPI.Data.DataTransferObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLContext _context;
        private IMapper _mapper;
        public ProductRepository(SQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> FindById(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id) ?? new Product();
            return _mapper.Map<ProductDTO>(product);
        }
        public async Task<ProductDTO> Create(ProductDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> Update(ProductDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Product? product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
                if(product.Id <= 0)return false;
                _context.Products!.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
