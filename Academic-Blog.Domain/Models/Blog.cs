using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string? Thumbnal_Url { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? ReviewDateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int View { get; set; } = 0;
        public string? ReviewFromReviewer { get; set; }
        public string? ShortDescription { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? ReviewerId { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual Account? Reviewer { get; set; } = null!;
        public virtual Account Author { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
