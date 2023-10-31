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
using Academic_Blog.Services.Implements;
using Microsoft.AspNetCore.OData.Query;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using Academic_Blog.PayLoad.Request.Account;
using Academic_Blog.Validatiors;
using Academic_Blog.PayLoad.Response;

namespace Academic_Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ODataController
    {

        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService , ILogger<AccountsController> logger)
        {
            _logger = logger;
            _accountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAccounts()
        {
          var accounts =  await _accountService.GetAccounts();
          if(accounts == null)
           {
                return NotFound();
           }
          return Ok(accounts);
        }
        [HttpGet("currentUser")]
        [EnableQuery(PageSize = 10)]
        [Authorize]
        public async Task<IActionResult> GetCurrentAccounts()
        {
           var account = await _accountService.GetCurrentAccount();
            return Ok(account);
        }
        [HttpPut("{id}/Ban")]
        [EnableQuery]
        [CustomAuthorize(Enums.RoleEnum.Admin)]
        public async Task<IActionResult> BannedAccount([FromRoute] Guid id,[FromBody] BannedAccountRequest request)
        {
            var isSuccessful = await _accountService.BanAnAccount(id, request);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok("successfully");
        }
        [HttpPost("unban")]
        [EnableQuery]
        [Authorize]
        public async Task<IActionResult> UnBannedAccount()
        {
            var isSuccessful = await _accountService.UnBanAnAccount();
            if (!isSuccessful)
            {
                return BadRequest(new ErrorResponse() {
                    StatusCode = StatusCodes.Status403Forbidden ,
                    Error = "Invalid update status",
                    TimeStamp = DateTime.Now
                });
            }
            return Ok("successfully");
        }
        /*
             // GET: api/Accounts/5
             [HttpGet("{id}")]
             public async Task<ActionResult<Account>> GetAccount(Guid id)
             {
               if (_context.Accounts == null)
               {
                   return NotFound();
               }
                 var account = await _context.Accounts.FindAsync(id);

                 if (account == null)
                 {
                     return NotFound();
                 }

                 return account;
             }

             // PUT: api/Accounts/5
             // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
           

             // POST: api/Accounts
             // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
             [HttpPost]
             public async Task<ActionResult<Account>> PostAccount(Account account)
             {
               if (_context.Accounts == null)
               {
                   return Problem("Entity set 'AcademicBlogContext.Accounts'  is null.");
               }
                 _context.Accounts.Add(account);
                 await _context.SaveChangesAsync();

                 return CreatedAtAction("GetAccount", new { id = account.Id }, account);
             }

             // DELETE: api/Accounts/5
             [HttpDelete("{id}")]
             public async Task<IActionResult> DeleteAccount(Guid id)
             {
                 if (_context.Accounts == null)
                 {
                     return NotFound();
                 }
                 var account = await _context.Accounts.FindAsync(id);
                 if (account == null)
                 {
                     return NotFound();
                 }

                 _context.Accounts.Remove(account);
                 await _context.SaveChangesAsync();

                 return NoContent();
             }

             private bool AccountExists(Guid id)
             {
                 return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
             } */
    }
}
