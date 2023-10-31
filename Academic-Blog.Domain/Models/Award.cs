using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class Award
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? IconUrl { get; set; }
        public virtual ICollection<AccountAwardMapping> AccountAwardMappings { get; set; } = new List<AccountAwardMapping>();
    }
}
