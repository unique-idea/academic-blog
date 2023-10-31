using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request.Blog;
using Academic_Blog.PayLoad.Request.Comment;
using Academic_Blog.PayLoad.Response.Blog;
using Academic_Blog.PayLoad.Response.Comment;
using AutoMapper;

namespace Academic_Blog.Mappers.Comments
{
    public class CommentMapper :Profile
    {
        public CommentMapper()
        {
            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<Comment,CommentResponse>();
        }
    }
}
