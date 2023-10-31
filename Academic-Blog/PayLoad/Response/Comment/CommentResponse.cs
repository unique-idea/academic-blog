namespace Academic_Blog.PayLoad.Response.Comment
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public Guid? ReplyToId { get; set; }
        public Guid CommentorId { get; set; }
        public Guid BlogId { get; set; }
    }
}
