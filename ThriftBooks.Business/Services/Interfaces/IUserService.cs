using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Users;

namespace ThriftBooks.Business.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAll(Expression<Func<UserModel, bool>> filter = null);
        UserModel GetById(Guid id);
        Task InsertAsync(CreateUserModel model);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(EditUserModel model);
        Task BuyBook(Guid userId, Guid bookId);
        UserModel GetUserWithBooks(Guid id);
        List<UserModel> GetAllUsersWithBooks(Expression<Func<UserModel, bool>> filter = null);
        UserAuthModel GetUserByUsername(string username);
        bool DoesUsernameExist(string username);

    }
}