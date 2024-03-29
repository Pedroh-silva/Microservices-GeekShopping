﻿using System.Net.Http.Headers;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IService;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private const string basePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(basePath);
            return await response.ReadContentAs<List<ProductModel>>();   
        }

        public async Task<ProductModel> FindByIdProduct(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{basePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> CreateProduct(ProductModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJsonAsync(basePath,model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong calling API");
        }
        public async Task<ProductModel> UpdateProduct(ProductModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJsonAsync(basePath, model);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductModel>();
            else throw new Exception("Something went wrong calling API"); 
        }
        public async Task<bool> DeleteProductById(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{basePath}/{id}");
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong calling API");
        }

    }
}
