using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Users;

namespace ThriftBooks.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(LoginModel model);
        Task<string> GenerateToken(UserAuthModel user);
    }
}
