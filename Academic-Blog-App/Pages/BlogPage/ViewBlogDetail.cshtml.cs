using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Response;
using Academic_Blog_App.Services.ClientAjax;
using Academic_Blog_App.Services.ClientEnum;
using Academic_Blog_App.Services.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Evaluation;
using Microsoft.Extensions.Internal;
using System.ComponentModel.Design;

namespace Academic_Blog_App.Pages.BlogPage
{
    public class ViewBlogDetailModel : PageModel
    {
        private readonly ApiHelper _apiHelper;
        private readonly CenterHub _centerHub;
        public ViewBlogDetailModel(ApiHelper apiHelper, CenterHub centerHub)
        {
            _apiHelper = apiHelper;
            _centerHub = centerHub;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;
        public List<Comment> Comments { get; set; } = default!;
        public List<Account> Accounts { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string? blogId, string? authorId)
        {
            if (blogId == null || authorId == null)
            {
                return RedirectToPage("/Index");
            }
            var acc = SessionHelper.GetObjectFromJson<LoginResponse>(HttpContext.Session, "Account");

            if (acc != null)
            {
                ViewData["Login"] = "True";
            }

            await FetchAll(blogId, authorId);
            return Page();
        }

        private async Task FetchAll(string? blogId, string? authorId)
        {
            var blogResult = await _apiHelper.FetchApiAsync<Blog>(EndPointEnum.Blogs, $"/{Guid.Parse(blogId!)}", MethodEnum.GET, null);
            var commentResult = await _apiHelper.FetchODataAsync<List<Comment>>(EndPointEnum.Comments, $"?$filter=blogId eq {Guid.Parse(blogId!)}");
            var accountResult = await _apiHelper.FetchApiAsync<List<Account>>(EndPointEnum.Accounts, "", MethodEnum.GET, null);
            if (blogResult.IsSuccess && commentResult.IsSuccess && accountResult.IsSuccess)
            {
                Blog = blogResult.Data;
                Comments = commentResult.Data;
                Accounts = accountResult.Data;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "CurrentBlog", Blog);
            }
            else
            {
                Error(blogResult.ErrorMessage + "\n" + commentResult.ErrorMessage + "\n" + accountResult.ErrorMessage);
            }
        }

        private IActionResult Error(string error)
        {
            TempData["Error"] = error;
            TempData["PageName"] = "ViewBlogDetails";
            return RedirectToPage("/Error");
        }
    }
}
