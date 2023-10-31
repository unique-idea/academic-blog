using Academic_Blog.Domain.Models;
using Academic_Blog_App.Services.ClientEnum;
using Academic_Blog_App.Services.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Academic_Blog_App.Pages.BlogPage
{
    public class ViewAllBlogModel : PageModel
    {

        private readonly ApiHelper _apiHelper;
        public ViewAllBlogModel(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        [BindProperty]
        public List<Blog> Blogs { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _apiHelper.FetchApiAsync<List<Blog>>(EndPointEnum.Blogs, "", MethodEnum.GET, null);
            if (result.IsSuccess)
            {
                Blogs = result.Data;
            }
            else
            {
                TempData["Error"] = result.ErrorMessage;
                TempData["PageName"] = "ViewAllBlog";
                return RedirectToPage("/Error");
            }
            return Page();
        }
    }
}
