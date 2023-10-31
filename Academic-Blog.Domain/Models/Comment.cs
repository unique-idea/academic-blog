using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public Guid? ReplyToId { get; set; }
        public Guid CommentorId {  get; set; }
        public Guid BlogId { get; set; }
        public virtual Comment? ReplyTo { get; set; }
        public virtual Account Commentor { get; set; } = null!;
        public virtual Blog Blog { get; set; } = null!;

    }
}
