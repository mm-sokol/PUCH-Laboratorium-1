using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureStoreApp.src.DTO;
using AdventureStoreApp.src.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace AdventureStoreApp.src.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProducts(); // Define your DTO as necessary
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            return products.Select(p => new ProductDto
                {
                    ProductID = p.ProductID,
                    ProductNumber = p.ProductNumber,
                    Name = p.Name,
                    StandardCost = p.StandardCost,
                    ListPrice = p.ListPrice,
                    Color = p.Color,
                    Size = p.Size,
                    Weight = p.Weight,
                    SellStartDate = p.SellStartDate,
                    ModifiedDate = p.ModifiedDate
                })
                .ToList();
        }
    }
}
