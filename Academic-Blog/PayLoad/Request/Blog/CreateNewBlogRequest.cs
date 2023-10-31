using Academic_Blog.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Academic_Blog.PayLoad.Request.Blog
{
    public class CreateNewBlogRequest
    {
        public string Thumbnal_Url { get; set; }
        [Required(ErrorMessage ="Title is not missing")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is not missing")]
        [MinLength(200,ErrorMessage = "Description is least 100 words")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is not missing")]

        public Guid CategoryId { get; set; }
    }
}
