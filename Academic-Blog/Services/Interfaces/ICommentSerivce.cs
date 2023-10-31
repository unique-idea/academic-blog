using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request.Comment;
using Academic_Blog.PayLoad.Response.Comment;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Academic_Blog.Services.Interfaces
{
    public interface ICommentSerivce
    {
      Task<CommentResponse> CreateComment(CreateCommentRequest request);
      Task<List<Comment>> GetComments();
       Task<bool> DeleteComments(Guid id);
    }
}
