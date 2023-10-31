using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Academic_Blog.Services.Interfaces;
using Microsoft.AspNetCore.OData.Query;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using Academic_Blog.PayLoad.Request.Comment;
using Academic_Blog.PayLoad.Response;

namespace Academic_Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly ICommentSerivce _commentService;
        public CommentsController(ICommentSerivce commentService,ILogger<CommentsController> logger)
        {
            _logger = logger;
            _commentService = commentService;
        }

        // GET: api/Comments
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetComments()
        {
           var response = await _commentService.GetComments();
           return Ok(response);
        }
        [HttpPost]
        [Authorize]
        [EnableQuery]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request)
        {
            var response = await _commentService.CreateComment(request);
            if (response == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = "Insert failed",
                    TimeStamp = DateTime.Now,
                });
            }
            return Ok(response);    
        }
        [HttpDelete("{id}")]
        [Authorize]
        [EnableQuery]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
           var isSuccess = await _commentService.DeleteComments(id);
           if(!isSuccess)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode= StatusCodes.Status400BadRequest,
                    Error = "Delete fail",
                    TimeStamp = DateTime.Now,
                });
            }
           return Ok("Success");
        }
        /*  // GET: api/Comments/5
          [HttpGet("{id}")]
          public async Task<ActionResult<Comment>> GetComment(Guid id)
          {
            if (_context.Comments == null)
            {
                return NotFound();
            }
              var comment = await _context.Comments.FindAsync(id);

              if (comment == null)
              {
                  return NotFound();
              }

              return comment;
          }

          // PUT: api/Comments/5
          // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
          [HttpPut("{id}")]
          public async Task<IActionResult> PutComment(Guid id, Comment comment)
          {
              if (id != comment.Id)
              {
                  return BadRequest();
              }

              _context.Entry(comment).State = EntityState.Modified;

              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!CommentExists(id))
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

          // POST: api/Comments
          // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

          // DELETE: api/Comments/5
        

          private bool CommentExists(Guid id)
          {
              return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
          }
        */
    }
}
