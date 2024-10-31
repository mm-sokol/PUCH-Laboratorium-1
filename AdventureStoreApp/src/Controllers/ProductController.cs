using Microsoft.AspNetCore.Mvc;
using AdventureStoreApp.src.Data;
using AdventureStoreApp.src.DTO;
using AdventureStoreApp.src.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;


namespace AdventureStoreApp.src.Controllers
{
    [Route("Products")]
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
            try {
                var products = await _productService.GetAllProducts();
                return View("~/Views/Products/Index.cshtml", products);

            } catch(Exception) {
                return View("~/Views/Error/Error.cshtml");
            }
        }  

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try {
                var product = await _productService.GetProductById(id);
                if (product==null)
                {
                    Console.WriteLine("odiejafoewqifpiwrqhfieurhfiuehrfoiure");
                    return NotFound();
                }
                return View("~/Views/Products/Details.cshtml");
            } catch(Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Internal server error");
            }
        }    
    }
}
