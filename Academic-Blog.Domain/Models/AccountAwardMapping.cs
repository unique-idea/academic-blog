using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class AccountAwardMapping
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public Guid LecturerId { get; set; }
        public Guid StudentId { get; set; }
        public Guid AwardId { get; set; }
        public virtual Account Lecturer { get; set; } = null!;
        public virtual Account Student { get; set; } = null!;
        public virtual Award Award { get; set; } = null!;
    }
}
