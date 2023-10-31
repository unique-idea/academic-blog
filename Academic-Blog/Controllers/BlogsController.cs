using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Academic_Blog.Services.Interfaces;
using Academic_Blog.Validatiors;
using Academic_Blog.PayLoad.Request.Blog;
using Academic_Blog.PayLoad.Response;
using Microsoft.AspNetCore.Authorization;

namespace Academic_Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ODataController
    {
        private readonly ILogger<BlogsController> _logger;
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService,ILogger<BlogsController> logger)
        {
            _blogService = blogService;
            _logger = logger;
        }

        // GET: api/Blogs
        [EnableQuery(PageSize =10)]       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
           var blogs = await _blogService.GetBlogs();
           if(blogs == null || blogs.Count == 0)
           {
                return NotFound("Not found any blogs");
           }
           return Ok(blogs);
        }

        // GET: api/Blogs/5
           [EnableQuery]
           [HttpGet("{id}")]
           public async Task<IActionResult> GetBlog(Guid id)
           {
              var response = await _blogService.ReadBlog(id);
              if(response == null)
              {
                return NotFound();
              }
            return Ok(response);
           }
         [EnableQuery(PageSize = 10)]
         [HttpGet("currentUser")]
        [CustomAuthorize(Enums.RoleEnum.Student)]
        public async Task<IActionResult> GetBlogsOfCurrentUser(string ? status)
        {
            var blogs = await _blogService.GetBlogOfCurrentUser(status);
            return Ok(blogs);
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableQuery]
           [HttpPut("student/{id}/edit")]
           [CustomAuthorize(Enums.RoleEnum.Student)]
           public async Task<IActionResult> UpdateBlogByStudent([FromRoute] Guid id,[FromBody] UpdateBlogRequest updateBlogRequest)
           {
            bool isSuccess = await _blogService.EditBlogByStudent(id, updateBlogRequest);
             if (!isSuccess) {
                return BadRequest("Update failed");
              }
            return Ok("Update successfully");
           }
        [EnableQuery]
        [HttpPut("{id}/censor")]
        [CustomAuthorize(Enums.RoleEnum.Lecturer)]
        public async Task<IActionResult> Censor([FromRoute] Guid id, [FromBody] CensorBlogRequest censorBlog)
        {
            bool isSuccess = await _blogService.CensorBlog(id,censorBlog);
            if (!isSuccess)
            {
                return BadRequest("Update failed");
            }
            return Ok("Update successfully");
        }
        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
           [EnableQuery]
           [HttpPost]
           [CustomAuthorize(Enums.RoleEnum.Student)]      
           public async Task<IActionResult> Create(CreateNewBlogRequest blog)
           {
            var blogResponse = await _blogService.CreateNewBlogs(blog);
             if(blogResponse == null)
            {
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = "You in banned in 48 hours",
                    TimeStamp = DateTime.Now,
                });
            }
             return CreatedAtAction("GetBlogs", new { id = blogResponse.Id }, blogResponse);
           }
           [EnableQuery]
           [HttpPut("{id}/delete")]
           [CustomAuthorize(Enums.RoleEnum.Lecturer,Enums.RoleEnum.Student)]
           public async Task<IActionResult> Delete(Guid id)
           {
            var isSuccessful = await _blogService.DeleteSoftBlog(id);
            if (!isSuccessful)
            {
                return BadRequest("Request Fail");
            }
            return Ok("Successfully");
           }
         /*
           // DELETE: api/Blogs/5
           [HttpDelete("{id}")]
            [EnableQuery]
           public async Task<IActionResult> DeleteBlog(Guid id)
           {
               if (_context.Blogs == null)
               {
                   return NotFound();
               }
               var blog = await _context.Blogs.FindAsync(id);
               if (blog == null)
               {
                   return NotFound();
               }

               _context.Blogs.Remove(blog);
               await _context.SaveChangesAsync();

               return NoContent();
           }

           private bool BlogExists(Guid id)
           {
               return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
           }*/
    }
}
