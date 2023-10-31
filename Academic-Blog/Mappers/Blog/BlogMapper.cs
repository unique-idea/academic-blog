using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request.Blog;
using Academic_Blog.PayLoad.Response.Blog;
using AutoMapper;

namespace Academic_Blog.Mappers.Blogs
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<CreateNewBlogRequest, Blog>();
            CreateMap<Blog,BlogResponse>();
        }
    }
}
