using System.ComponentModel.DataAnnotations;

namespace Academic_Blog.PayLoad.Request.Blog
{
    public class UpdateBlogRequest
    {
        public string Thumbnal_Url { get; set; }
        [Required(ErrorMessage = "Title is not missing")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is not missing")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is not missing")]

        public Guid CategoryId { get; set; }
    }
}
