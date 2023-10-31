using Academic_Blog.Domain.Migrations;
using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request.Blog;
using Academic_Blog.PayLoad.Response.Blog;

namespace Academic_Blog.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetBlogs();
        Task<BlogResponse> CreateNewBlogs(CreateNewBlogRequest request);
        Task<bool> EditBlogByStudent(Guid id,UpdateBlogRequest request);
        Task<bool> CensorBlog(Guid id, CensorBlogRequest request);
        Task<BlogResponse> ReadBlog(Guid id);
        Task<bool> DeleteSoftBlog(Guid id);
        Task<List<Blog>> GetBlogOfCurrentUser(string? status);
        Task<bool> IncreaseView(Guid id);
    }
}
