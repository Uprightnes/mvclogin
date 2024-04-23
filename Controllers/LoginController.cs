using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LMVC.Models;

namespace LMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await AuthenticateUser(model);
                Console.WriteLine(response);
                if (response != null)
                {
                    TempData["AuthenticationResponse"] = JsonConvert.SerializeObject(response);
                    return RedirectToAction("Index", "Success");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    return RedirectToAction("Index", "ErrorPage");
                }
            }
            return View(model);
        }

        private async Task<AuthenticationResponse> AuthenticateUser(LoginViewModel model)
        {
            string externalEndpointUrl = "https://map.phed.com.ng/api/PHEDConnectApi/Login";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync(externalEndpointUrl, model);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<AuthenticationResponse>(responseBody);
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = errorResponse;
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return null;
            }
        }
    }
}
