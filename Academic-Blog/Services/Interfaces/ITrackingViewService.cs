using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request.TrackingViewBlog;

namespace Academic_Blog.Services.Interfaces
{
    public interface ITrackingViewService
    {
        Task<List<TrackingViewBlog>> GetTrackingViewBlogs();
        Task<bool> CreateTrackingViewBlog(TrackingViewBlogRequest tracking);
    }
}
