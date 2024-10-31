using Microsoft.AspNetCore.Mvc;


namespace AdventureStoreApp.Controllers
{
    public class ErrorController : Controller
    {
        
        [Route("Error")]
        public IActionResult Index()
        {
            return View("Error");
        }                
        
        // public IActionResult Error(int statusCode)
        // {
        //     switch (statusCode)
        //     {
        //         case 404:
        //             return View("NotFound"); // Create a NotFound.cshtml view
        //         case 500:
        //             return View("ServerError"); // Create a ServerError.cshtml view
        //         default:
        //             return View();
        //     }
        // }
    }

}
