using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Request;
using Academic_Blog.PayLoad.Request.Account;
using Academic_Blog.PayLoad.Response;

namespace Academic_Blog.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<List<Account>> GetAccounts();
        Task<Account> GetCurrentAccount();
        Task<bool> BanAnAccount(Guid accountId,BannedAccountRequest request);
        Task<bool> UnBanAnAccount();
    }
}
