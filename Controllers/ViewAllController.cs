using Microsoft.AspNetCore.Mvc;
using LMVC.Models;

namespace LMVC.Controllers
{
    public class ViewAllController : Controller
    {
        // GET: Success
        public IActionResult Index(AuthenticationResponse response)
        {
            return View(response);
        }
    }
}
