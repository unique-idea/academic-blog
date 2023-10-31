using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Academic_Blog.Repository.Interfaces;
using Academic_Blog.Services.Interfaces;
using AutoMapper;

namespace Academic_Blog.Services.Implements
{
    public class CategoryService :  BaseService<CategoryService>, ICategoryService
    {
        public CategoryService(IUnitOfWork<AcademicBlogContext> unitOfWork, ILogger<CategoryService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<List<Category>> GetCategories()
        {
            ICollection<Category> categorys = await _unitOfWork.GetRepository<Category>().GetListAsync(predicate: x => x.Id == x.Id);
            List<Category> categories = categorys.ToList();
            return categories;
        }
    }
}
