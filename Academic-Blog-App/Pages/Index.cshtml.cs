using Academic_Blog.PayLoad.Response;
using Academic_Blog_App.Services.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Academic_Blog_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public LoginResponse LoginResponse { get; set; }

        public void OnGet()
        {
            var loginAccount = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "Account");
            LoginResponse = loginAccount;
        }
    }
}