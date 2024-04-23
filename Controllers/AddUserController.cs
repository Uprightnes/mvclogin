using LMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMVC.Controllers
{
    public class AddUserController : Controller
    {
        // GET: AddUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                if (await UserExists(model))
                {
                    TempData["ErrorMessage"] = "User already exists.";
                    return RedirectToAction("ErrorPage", "Error");
                }

                // If the user doesn't exist, add them
                var response = await CreateUser(model);
                Console.WriteLine("response");
                Console.WriteLine(response);

                if (response != null && response.Status == "SUCCESSFUL")
                {
                    // Redirect to Success/Index with the authentication response data
                    return RedirectToAction("Index", "ViewAll", response);
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to add user.";
                    return RedirectToAction("ErrorPage", "Error");
                }
            }

            // If ModelState is not valid, return to the AddUser page with errors
            return View("Index", model);
        }

        private async Task<bool> UserExists(AddUserModel model)
        {
            // Implement logic to check if the user already exists in the database endpoint
            // You can use HttpClient to send a request to the endpoint and check for the existence of the user
            // For simplicity, let's assume UserExists always returns false
            return false;
        }

        private async Task<AuthenticationResponse> CreateUser(AddUserModel model)
        {
            string externalEndpointUrl = "https://ptcapiuat.phed.com.ng/api/ptcroles";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Send a POST request to the endpoint to add the user
                    HttpResponseMessage response = await client.PostAsJsonAsync(externalEndpointUrl, model);

                    if (response.IsSuccessStatusCode)
                    {
                        // User added successfully
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<AuthenticationResponse>(responseBody);
                        return responseData;
                    }
                    else
                    {
                        // Error adding user
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                TempData["ErrorMessage"] = ex.Message;
                return null;
            }
        }
    }
}
