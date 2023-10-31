
using Academic_Blog.Domain;
using Academic_Blog.Repository.Implement;
using Academic_Blog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Academic_Blog_App.Services
{
    public static class DependencyInjection
    {
        public static void InjectInfracstucture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcademicBlogContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultDB")));
            //
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //


        }

        public static void InjectService(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddRazorPages()
                .AddJsonOptions(options =>
                {

                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                })
                .AddRazorRuntimeCompilation();
            //
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }
    }
}
