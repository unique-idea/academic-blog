using System.ComponentModel.DataAnnotations;

namespace Academic_Blog.PayLoad.Request.TrackingViewBlog
{
    public class TrackingViewBlogRequest
    {
        [Required]
        public string TrackingId { get; set; }
        [Required]
        public Guid BlogId { get; set; }

    }
}
