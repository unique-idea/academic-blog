using System.ComponentModel.DataAnnotations;

namespace Academic_Blog.PayLoad.Request.Comment
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage ="Content is missing")]
        [MaxLength(255)] 
        public string Content { get; set; }
        public Guid? ReplyToId { get; set; }    
        public Guid BlogId { get; set; }


    }
}
