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
using Academic_Blog.Services.Interfaces;
using Microsoft.AspNetCore.OData.Query;
using Academic_Blog.PayLoad.Request.TrackingViewBlog;
using Academic_Blog.PayLoad.Response;

namespace Academic_Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingViewBlogsController : ODataController
    {

        private readonly ILogger<TrackingViewBlogsController> _logger;
        private readonly ITrackingViewService _trackingService;
        private readonly IBlogService _blogService; 
        public TrackingViewBlogsController(ITrackingViewService trackingService, ILogger<TrackingViewBlogsController> logger, IBlogService blogService)
        {
            _logger = logger;
            _trackingService = trackingService;
            _blogService = blogService;
        }


        // GET: api/TrackingViewBlogs
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetTrackingViewBlogs()
        {
           var trackings = await _trackingService.GetTrackingViewBlogs();
            return Ok(trackings);
        }
        // POST: api/TrackingViewBlogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> CreaateTrackingViewBlog(TrackingViewBlogRequest trackingViewBlog)
        {
           var isSuccessfully = await _trackingService.CreateTrackingViewBlog(trackingViewBlog);
            if (!isSuccessfully)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Some thing was wrong",
                    StatusCode = StatusCodes.Status400BadRequest,
                    TimeStamp = DateTime.Now
                });
            }
            await _blogService.IncreaseView(trackingViewBlog.BlogId);
            return Ok("successfully");
        }
        /*
        // GET: api/TrackingViewBlogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackingViewBlog>> GetTrackingViewBlog(int id)
        {
          if (_context.TrackingViewBlogs == null)
          {
              return NotFound();
          }
            var trackingViewBlog = await _context.TrackingViewBlogs.FindAsync(id);

            if (trackingViewBlog == null)
            {
                return NotFound();
            }

            return trackingViewBlog;
        }

        // PUT: api/TrackingViewBlogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackingViewBlog(int id, TrackingViewBlog trackingViewBlog)
        {
            if (id != trackingViewBlog.Id)
            {
                return BadRequest();
            }

            _context.Entry(trackingViewBlog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackingViewBlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrackingViewBlogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrackingViewBlog>> PostTrackingViewBlog(TrackingViewBlog trackingViewBlog)
        {
          if (_context.TrackingViewBlogs == null)
          {
              return Problem("Entity set 'AcademicBlogContext.TrackingViewBlogs'  is null.");
          }
            _context.TrackingViewBlogs.Add(trackingViewBlog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrackingViewBlog", new { id = trackingViewBlog.Id }, trackingViewBlog);
        }

        // DELETE: api/TrackingViewBlogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackingViewBlog(int id)
        {
            if (_context.TrackingViewBlogs == null)
            {
                return NotFound();
            }
            var trackingViewBlog = await _context.TrackingViewBlogs.FindAsync(id);
            if (trackingViewBlog == null)
            {
                return NotFound();
            }

            _context.TrackingViewBlogs.Remove(trackingViewBlog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrackingViewBlogExists(int id)
        {
            return (_context.TrackingViewBlogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    */
    }
}
