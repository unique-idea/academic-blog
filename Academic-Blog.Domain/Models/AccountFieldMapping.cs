using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class AccountFieldMapping
    {
        public Guid Id { get; set; }
        public Guid FieldId { get; set; }
        public Guid AccountId { get; set; } 
        public virtual Field Field { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}
