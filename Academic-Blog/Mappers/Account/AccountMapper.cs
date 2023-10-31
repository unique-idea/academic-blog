using Academic_Blog.Domain.Models;
using Academic_Blog.PayLoad.Response;
using AutoMapper;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Academic_Blog.Mappers.AccountMapper
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, LoginResponse>()
                .ForMember(des => des.AccessToken, src => src.Ignore())
                .ForMember(des => des.Role, src => src.MapFrom(src => src.Role.Name));

        }
    }
}
