using Academic_Blog.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace Academic_Blog_App.Pages.BlogPage
{
    public class CreateNewBlogModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string blogUrl;

        public CreateNewBlogModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            blogUrl = "https://localhost:5047/api/Blogs";
        }

        public void OnGet()
        {
        }
    }
}
