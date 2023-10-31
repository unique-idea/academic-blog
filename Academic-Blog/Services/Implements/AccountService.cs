using Academic_Blog.Domain;
using Academic_Blog.Domain.Models;
using Academic_Blog.Enums;
using Academic_Blog.PayLoad.Request;
using Academic_Blog.PayLoad.Request.Account;
using Academic_Blog.PayLoad.Response;
using Academic_Blog.Repository.Interfaces;
using Academic_Blog.Services.Interfaces;
using Academic_Blog.Utils;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Academic_Blog.Services.Implements
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        public AccountService(IUnitOfWork<AcademicBlogContext> unitOfWork, ILogger<AccountService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<bool> BanAnAccount(Guid accountId,BannedAccountRequest request)
        {
            var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate : x => x.Id == accountId);
            if (account == null)
            {
                return false;
            }
            account.Status = AccountStatusEnum.BANNED.GetDescriptionFromEnum<AccountStatusEnum>();
            _unitOfWork.GetRepository<Account>().UpdateAsync(account);
            BannedInfor bannedInfor = new BannedInfor
            {
                AccountId = accountId,
                DateBanned = DateTime.Now,
                Reason = request.Reason,
                Id = Guid.NewGuid(),
            };
            await _unitOfWork.GetRepository<BannedInfor>().InsertAsync(bannedInfor);
            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;
            return isSuccessfully;
        }

        public async Task<List<Account>> GetAccounts()
        {

            ICollection<Account> accounts = await _unitOfWork.GetRepository<Account>().GetListAsync(predicate: x => x.Id == x.Id);
            List<Account> result = accounts.ToList();
            return result;
        }

        public async Task<Account> GetCurrentAccount()
        {
            var account = await GetUserFromJwt();
            return account;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            Expression<Func<Account, bool>> accountFilter = p =>
                 p.UserName.Equals(loginRequest.Username) &&
                 p.Password.Equals(loginRequest.Password);
            Account account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate: accountFilter,include : p => p.Include(x => x.Role));
            if(account == null)
            {
                return null;
            }
            string Role = account.Role.Name;
            Tuple<string, Guid> guidClaim = new Tuple<string,Guid>("userId",account.Id);
            var token = JwtUtil.GenerateJwtToken(account, guidClaim);
            LoginResponse loginResponse = new LoginResponse
            {
                AccessToken = token,
                Id = account.Id,
                Username = account.UserName,
                Name = account.Name,
                Role = Role,
                AccountStatus = account.Status,
            };
           return loginResponse;
        }

        public async Task<bool> UnBanAnAccount()
        {
            var account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(predicate: x => x.Id == GetUserIdFromJwt());
            var bannedInfor = await _unitOfWork.GetRepository<BannedInfor>().GetListAsync(predicate : x => x.AccountId == account.Id,orderBy : x => x.OrderByDescending(x => x.DateBanned));
            var date = bannedInfor.FirstOrDefault()?.DateBanned;
            if (!account.Status.Equals(AccountStatusEnum.BANNED.GetDescriptionFromEnum<AccountStatusEnum>()))
            {
                throw new BadHttpRequestException("Your status is active , can't unban", StatusCodes.Status400BadRequest);
            }
            TimeSpan diff = DateTime.Now - date.GetValueOrDefault();
            if (diff.TotalDays < 2)
            {
                throw new BadHttpRequestException("Your time you banned is valid",StatusCodes.Status400BadRequest);
            }
            account.Status = AccountStatusEnum.ACTIVE.GetDescriptionFromEnum<AccountStatusEnum>();
            _unitOfWork.GetRepository<Account>().UpdateAsync(account);
            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;
            return isSuccessfully;
        }
    }
}
