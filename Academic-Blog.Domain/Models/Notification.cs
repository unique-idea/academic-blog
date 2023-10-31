using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class Notification
    {
        public Guid Id { get; set; }    
        public DateTime DateTime { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string Type { get; set; } = null!;
        public Guid? FromUserId { get; set; }
        public Guid ForUserId { get; set; }
        public Account? FromUser { get; set; }
        public Account ForUser { get; set; } = null!;
    }
}
