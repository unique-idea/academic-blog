using System.ComponentModel.DataAnnotations;

namespace Academic_Blog.PayLoad.Request.Blog
{
    public class CensorBlogRequest
    {
        [Required(ErrorMessage ="Status is not missing")]
        public string Status { get; set; }
        [MaxLength(1024)]
        public string ReviewFromReviewer { get; set; }
    }
}
