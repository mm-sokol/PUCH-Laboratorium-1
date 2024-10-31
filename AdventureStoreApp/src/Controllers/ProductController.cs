using Microsoft.AspNetCore.Mvc;
using AdventureStoreApp.src.Data;
using AdventureStoreApp.src.DTO;
using AdventureStoreApp.src.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace AdventureStoreApp.src.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View("~\\src\\Views\\Products\\Index.cshtml", products);
        }    
    }
}
