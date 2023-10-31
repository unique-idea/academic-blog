using System.ComponentModel.DataAnnotations;

namespace Academic_Blog.PayLoad.Request.Account
{
    public class BannedAccountRequest
    {
        [MaxLength(255)]
        public string Reason { get; set; }  
    }
}
