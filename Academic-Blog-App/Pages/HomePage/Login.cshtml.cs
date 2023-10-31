using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request;
using Academic_Blog.PayLoad.Response;
using Academic_Blog_App.Services.Helper;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Academic_Blog_App.Pages.HomePage
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string accountUrl;

        public LoginModel() {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            accountUrl = "http://localhost:5047/api/Authentication/login";
        }

        [BindProperty]
        public string UserName { get; set;}
        [BindProperty]
        public string Password { get; set;}

        [BindProperty]
        public List<Account> ListAccounts { get; set; } = default!;

        //public async Task<IActionResult> OnGetAsync() {
        //    HttpResponseMessage response = await _httpClient.GetAsync(accountUrl +
        //        "?$filter=Name eq 'desiredName' and Password eq 'desiredPassword'");

        //    if (response.IsSuccessStatusCode) {
        //        string content = await response.Content.ReadAsStringAsync();
        //        var options = new JsonSerializerOptions {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        ListAccounts = JsonSerializer.Deserialize<List<Account>>(content, options)!;
        //    }
        //    else {
        //        ViewData["Error"] = response.ToString();
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync() {
            // Get admin account
            bool isAdmin = getAdminAccount(UserName, Password);


            try {
                if(!isAdmin) {
                    var jsonContent = JsonSerializer.Serialize(new LoginRequest {
                        Username = UserName, Password = Password
                    });
                    
                    HttpResponseMessage response = await _httpClient.PostAsync(accountUrl, 
                        new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode) {
                        string content = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions {
                            PropertyNameCaseInsensitive = true,
                        };
                        var account = JsonSerializer.Deserialize<LoginResponse>(content, options);
                        if(account == null) {
                            ViewData["ErrorLoginMessage"] = "Incorrect user name or password";
                            return Page();
                        }
                        else {
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "Account", account);
                            return RedirectToPage("/Index");
                        }
                    }
                }
            }
            catch (Exception) {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("/AdminPage/DashBoard");
        }

        private bool getAdminAccount(string? UserName, string? Password) {
            string json = string.Empty;

            try {
                // read json from file
                using (StreamReader sr = new StreamReader(@"..\Academic-Blog\appsettings.json")) {
                    json = sr.ReadToEnd();
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();

                // Convert json string to dynamic type
                var obj = jss.Deserialize<dynamic>(json);

                // Get content
                string userName = obj["AdminAccount"]["UserName"];
                string password = obj["AdminAccount"]["Password"];

                if(userName.Equals(UserName, StringComparison.OrdinalIgnoreCase) && 
                    password.Equals(Password, StringComparison.OrdinalIgnoreCase)) {
                    return true;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
