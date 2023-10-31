using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Academic_Blog.Enums;
using Academic_Blog.PayLoad.Request.Blog;
using Academic_Blog.PayLoad.Response.Blog;
using Academic_Blog.Repository.Interfaces;
using Academic_Blog.Services.Interfaces;
using Academic_Blog.Utils;
using AutoMapper;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using System.Reflection.Metadata;

namespace Academic_Blog.Services.Implements
{
    public class BlogService : BaseService<BlogService>, IBlogService
    {
        public BlogService(IUnitOfWork<AcademicBlogContext> unitOfWork, ILogger<BlogService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<bool> CensorBlog(Guid id, CensorBlogRequest request)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(predicate: x => x.Id == id);
            if (blog == null)
            {
                return false;
            }
            if (!blog.Status.Equals(Enums.BlogStatus.PENDING.ToString()))
            {
                return false;
            }
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(predicate : x => x.Id == blog.CategoryId);
            var fieldId = await _unitOfWork.GetRepository<Field>().SingleOrDefaultAsync(predicate: x => x.Id == category.FieldId,selector : x => x.Id);
            var account = await GetUserFromJwt();
            if(account?.AccountFieldMapping?.FieldId != fieldId)
            {
                return false;
            }
            blog.Status = request.Status;
            blog.ReviewerId = GetUserIdFromJwt();
            blog.ReviewDateTime = DateTime.Now;
            blog.UpdatedTime = DateTime.Now;
            blog.ReviewFromReviewer = request.ReviewFromReviewer;
            if(request.Status == Enums.BlogStatus.APPROVED.ToString())
            {
                var acc = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate: x => x.Id == blog.AuthorId);
                account.NumberOfBlogs = account.NumberOfBlogs + 1;
                _unitOfWork.GetRepository<Account>().UpdateAsync(account);
            }
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<BlogResponse> CreateNewBlogs(CreateNewBlogRequest request)
        {
            Blog blog = _mapper.Map<Blog>(request);
            var account = await GetUserFromJwt();
            if (account.Status.Equals(AccountStatusEnum.BANNED.GetDescriptionFromEnum<AccountStatusEnum>()))
            {
                throw new BadHttpRequestException("You in banned time", StatusCodes.Status400BadRequest);
            }
            blog.Status = (Enums.BlogStatus.PENDING.GetDescriptionFromEnum());
            blog.UpdatedTime = DateTime.Now;
            blog.CreatedTime = DateTime.Now;
            blog.AuthorId = GetUserIdFromJwt();
            blog.View = 0;
            blog.Id = Guid.NewGuid();
            blog.Description = request.Description;
            blog.ShortDescription = blog.Description.Substring(0,200) + "...";
            await _unitOfWork.GetRepository<Blog>().InsertAsync(blog);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            BlogResponse response = null;
            if(isSuccessful)
            {
               response = _mapper.Map<BlogResponse>(blog);
            }
            return response;
        }

        public async Task<bool> DeleteSoftBlog(Guid id)
        {
            var role = GetRoleFromJwt();
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(predicate: x => x.Id == id);
            if (role.Equals(RoleEnum.Student.GetDescriptionFromEnum<RoleEnum>()))
            {
                if(GetUserIdFromJwt() != blog.AuthorId)
                {
                    return false;
                }
                if(blog == null)
                {
                    return false;
                }
                if (!blog.Status.Equals(BlogStatus.PENDING.GetDescriptionFromEnum<BlogStatus>()))
                {
                    return false;
                }

            }
            if (role.Equals(RoleEnum.Lecturer.GetDescriptionFromEnum<RoleEnum>()))
            {
                if (blog == null)
                {
                    return false;
                }
                if (blog.Status.Equals(BlogStatus.APPROVED.GetDescriptionFromEnum<BlogStatus>()))
                {
                    var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate: x => x.Id == blog.AuthorId);
                    account.NumberOfBlogs = account.NumberOfBlogs - 1;
                    _unitOfWork.GetRepository<Account>().UpdateAsync(account);
                }
            }
            blog.Status = BlogStatus.DELETED.GetDescriptionFromEnum<BlogStatus>();
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            var isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<bool> EditBlogByStudent(Guid id, UpdateBlogRequest request)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(predicate : x => x.Id == id);
            if(blog == null)
            {
                return false;
            }
            if(GetUserIdFromJwt() != blog.AuthorId)
            {
                return false;
            }
            if(!blog.Status.Equals(Enums.BlogStatus.PENDING.ToString())){
              return false;
            }
            blog.Title = request.Title;
            blog.Description = request.Description;
            blog.UpdatedTime = DateTime.Now;
            blog.Thumbnal_Url  = request.Thumbnal_Url;
            blog.CategoryId = request.CategoryId;
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<List<Blog>> GetBlogOfCurrentUser(string? status)
        {
            var guid = GetUserIdFromJwt();
            List<Blog> blog = null;
            if (status != null)
            {
              var blg  = await _unitOfWork.GetRepository<Blog>().GetListAsync(predicate: x => x.AuthorId == guid && x.Status.Equals(status));
              blog = blg.ToList() ?? new List<Blog>();
            }else
            {
                var blg = await _unitOfWork.GetRepository<Blog>().GetListAsync(predicate: x => x.AuthorId == guid);
                blog = blg.ToList() ?? new List<Blog>();
            }
            return blog;
        }

        public async Task<List<Blog>> GetBlogs()
        {
            ICollection<Blog> blogs = await _unitOfWork.GetRepository<Blog>().GetListAsync(predicate : x => x.Id == x.Id);
            List<Blog> result = blogs.ToList();
            return result;
        }

        public async Task<BlogResponse> ReadBlog(Guid id)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(predicate : x => x.Id == id && x.Status == BlogStatus.APPROVED.GetDescriptionFromEnum<BlogStatus>());
            if (blog == null)
            {
                return null;
            }
            var response = _mapper.Map<BlogResponse>(blog);
            return response;
        }

        public async Task<bool> IncreaseView(Guid id)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().SingleOrDefaultAsync(predicate: x => x.Id == id && x.Status == BlogStatus.APPROVED.GetDescriptionFromEnum<BlogStatus>());
            if (blog == null)
            {
                return false;
            }
            blog.View += 1;
            _unitOfWork.GetRepository<Blog>().UpdateAsync(blog);
            var isSuccessfull = await _unitOfWork.CommitAsync() >0;
            return isSuccessfull;
        }
    }
}
