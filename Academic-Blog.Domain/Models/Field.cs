using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public virtual ICollection<AccountFieldMapping> AccountFieldMappings { get; set; } = new List<AccountFieldMapping>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();   
    }
}
