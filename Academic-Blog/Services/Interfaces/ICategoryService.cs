using Academic_Blog.Domain.Models;

namespace Academic_Blog.Services.Interfaces
{
    public interface ICategoryService
    {
       Task<List<Category>> GetCategories();
    }
}
