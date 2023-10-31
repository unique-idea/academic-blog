using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request.TrackingViewBlog;
using Academic_Blog.Repository.Interfaces;
using Academic_Blog.Services.Interfaces;
using AutoMapper;

namespace Academic_Blog.Services.Implements
{
    public class TrackingViewBlogService : BaseService<TrackingViewBlogService>, ITrackingViewService
    {
        public TrackingViewBlogService(IUnitOfWork<AcademicBlogContext> unitOfWork, ILogger<TrackingViewBlogService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<bool> CreateTrackingViewBlog(TrackingViewBlogRequest tracking)
        {
            if(tracking == null)
            {
                return false;
            }
            var isExist = (await _unitOfWork.GetRepository<TrackingViewBlog>().SingleOrDefaultAsync(predicate : x => x.TrackingId == tracking.TrackingId &&  x.BlogId == tracking.BlogId));
            if (isExist != null)
            {
                return false;
            }
            TrackingViewBlog trackingViewBlog = new TrackingViewBlog
            {
                BlogId = tracking.BlogId,
                TrackingId = tracking.TrackingId,
            };
            await _unitOfWork.GetRepository<TrackingViewBlog>().InsertAsync(trackingViewBlog);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<List<TrackingViewBlog>> GetTrackingViewBlogs()
        {
            List<TrackingViewBlog> trackingViewBlogs  = (await _unitOfWork.GetRepository<TrackingViewBlog>().GetListAsync(predicate: x => x.Id == x.Id)).ToList();
            return trackingViewBlogs;
        }
    }
}
