using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class BannedInfor
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = null!;
        public Guid AccountId { get; set; }
        public DateTime DateBanned { get; set; }
        public virtual Account Account { get; set; } = null!;
    }
}
