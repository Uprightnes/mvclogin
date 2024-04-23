using Microsoft.AspNetCore.Mvc;
using LMVC.Models;

namespace LMVC.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("seen it");
         return View();
        }
    }
}