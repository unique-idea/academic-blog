using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Academic_Blog.Enums;
using Academic_Blog.PayLoad.Request.Comment;
using Academic_Blog.PayLoad.Response.Comment;
using Academic_Blog.Repository.Interfaces;
using Academic_Blog.Services.Interfaces;
using Academic_Blog.Utils;
using AutoMapper;

namespace Academic_Blog.Services.Implements
{
    public class CommentService : BaseService<CommentService>, ICommentSerivce
    {
        public CommentService(IUnitOfWork<AcademicBlogContext> unitOfWork, ILogger<CommentService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CommentResponse> CreateComment(CreateCommentRequest request)
        {
            Comment comment = _mapper.Map<Comment>(request);
            var account = await GetUserFromJwt();
            if (account.Status.Equals(AccountStatusEnum.BANNED.GetDescriptionFromEnum<AccountStatusEnum>()))
            {
                throw new BadHttpRequestException("You in banned time", StatusCodes.Status400BadRequest);
            }
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(predicate: x => x.Id == request.BlogId);
            if (!blog.Status.Equals(BlogStatus.APPROVED.GetDescriptionFromEnum<BlogStatus>()))
            {
                throw new BadHttpRequestException("blog is not active", StatusCodes.Status400BadRequest);
            }
            comment.ReplyToId = request.ReplyToId;
            comment.CommentorId = new Guid();
            comment.CreateTime = DateTime.Now;
            comment.BlogId = request.BlogId;
            comment.Id = Guid.NewGuid();
            comment.Content = request.Content;
            await _unitOfWork.GetRepository<Comment>().InsertAsync(comment);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            CommentResponse response = null;
            if (isSuccessful) {
                response =  _mapper.Map<CommentResponse>(comment);
            }
            return response;
        }

        public async Task<bool> DeleteComments(Guid id)
        {
            var userId = GetUserIdFromJwt();
            var comment = await _unitOfWork.GetRepository<Comment>().SingleOrDefaultAsync(predicate : x => x.Id == id);
            if(comment.CommentorId !=  userId)
            {
                throw new BadHttpRequestException("This Comment is not belongs of you",StatusCodes.Status400BadRequest);
            }
            if (comment == null)
            {
                return false;
            }
            var commentsRelated = await _unitOfWork.GetRepository<Comment>().GetListAsync(predicate : x => x.ReplyToId == id);
            _unitOfWork.GetRepository<Comment>().DeleteAsync(comment);
            _unitOfWork.GetRepository<Comment>().DeleteRangeAsync(commentsRelated);
            var isSuccess = await _unitOfWork.CommitAsync() > 0;
            return isSuccess;   
        }

        public async Task<List<Comment>> GetComments()
        {
            List<Comment> comments = (await _unitOfWork.GetRepository<Comment>().GetListAsync(predicate : x => x.Id == x.Id)).ToList();
            return comments;
        }
    }
}
