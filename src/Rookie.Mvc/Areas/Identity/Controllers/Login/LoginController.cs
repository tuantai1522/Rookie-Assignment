using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Identity.Models.Login;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Areas.Identity.Controllers.Login
{
    [Area("Identity")]
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        public LoginController()
        {
            _client = new HttpClient();
            _client.BaseAddress = Utilities.BASE_ADDRESS;

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDto data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Authenticate with the API to get the JWT token
                    var jsonContent = JsonConvert.SerializeObject(data);
                    StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client
                                                .PostAsync(_client.BaseAddress + $"/user/LoginUser",
                                                                            stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string info = await response.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(info))
                        {
                            if (doc.RootElement.TryGetProperty("token", out JsonElement tokenElement))
                            {
                                string tokenString = tokenElement.GetString();

                                // Save the token in a secure HTTP-only cookie
                                Response.Cookies.Append("Jwt", tokenString, new CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true, // Set to true if using HTTPS
                                    SameSite = SameSiteMode.Strict // Adjust as needed
                                });


                                JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);


                                if (doc.RootElement.TryGetProperty("userName", out JsonElement userNameElement))
                                {
                                    string userName = userNameElement.GetString();

                                    // Create claims and sign in the user
                                    var claims = new List<Claim>
                                {
                                    new (JwtRegisteredClaimNames.Name, doc.RootElement.GetProperty("firstName").GetString() ?? "firstName"),
                                    new (JwtRegisteredClaimNames.FamilyName, doc.RootElement.GetProperty("lastName").GetString() ?? "lastName"),
                                    new (JwtRegisteredClaimNames.UniqueName, userName),
                                    new (JwtRegisteredClaimNames.Email, doc.RootElement.GetProperty("email").GetString() ?? "email"),
                                    new("id", doc.RootElement.GetProperty("id").GetString() ?? "id"),
                                };

                                    // Extract and add roles claims
                                    var roles = token.Claims.FirstOrDefault(x => x.Type == "role")?.Value;

                                    if (!string.IsNullOrEmpty(roles))
                                        claims.AddRange(roles
                                                        .Split(',')
                                                        .Select(role => new Claim(ClaimTypes.Role, role)));

                                    var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                                    var principal = new ClaimsPrincipal(claimsIdentity);

                                    await HttpContext.SignInAsync(
                                        CookieAuthenticationDefaults.AuthenticationScheme,
                                        principal,
                                        new AuthenticationProperties
                                        {
                                            IsPersistent = true,
                                            AllowRefresh = true,
                                            ExpiresUtc = DateTime.UtcNow.AddDays(1)
                                        });

                                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Your username or password is not correct.";
                        return View("Index", data);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                    return View("Index", data);
                }
            }

            ViewBag.ErrorMessage = "Please provide complete information.";
            return View("Index", data);
        }
    }
}