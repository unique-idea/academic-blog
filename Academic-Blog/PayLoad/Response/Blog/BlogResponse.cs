namespace Academic_Blog.PayLoad.Response.Blog
{
    public class BlogResponse
    {
        public Guid Id { get; set; }
        public string? Thumbnal_Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? ReviewDateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int View { get; set; } 
        public Guid CategoryId { get; set; }
        public Guid? ReviewerId { get; set; }
        public Guid AuthorId { get; set; }
        public string? ShortDescription { get; set; }
    }
}
