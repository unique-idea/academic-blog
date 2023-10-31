using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Academic_Blog.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Gmail { get; set; } = null!;
        public int? NumberOfBlogs { get; set; }
        public Guid RoleId { get; set; }
        public Guid? AccountFieldMappingId { get; set; }
        public String Avatar { get; set; }  
        //public Guid? BannedInforId { get; set; }
        public virtual Role Role { get; set; } = null!;
       // public virtual BannedInfor? BannedInfor { get; set; }
        public ICollection<BannedInfor>  BannedInfors { get; set; } = new List<BannedInfor>();
        public virtual AccountFieldMapping? AccountFieldMapping { get; set; } 
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<AccountAwardMapping> StudentAwardMappings { get; set; } = new List<AccountAwardMapping>();
        public virtual ICollection<AccountAwardMapping> LecturerAwardMappings { get; set; } = new List<AccountAwardMapping>();
        public virtual ICollection<Notification> MyNotifications { get; set; } = new List<Notification>();
        public virtual ICollection<Notification> MyImpactsNotifications { get; set; } = new List<Notification>();
        public virtual ICollection<Blog> AuthorBlogs { get; set; } = new List<Blog>();
        public virtual ICollection<Blog> ReviewBlogs { get; set; } = new List<Blog>();  

    }
}
