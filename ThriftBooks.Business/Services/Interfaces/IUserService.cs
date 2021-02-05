using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Users;

namespace ThriftBooks.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task DeleteAsync(Guid id);
        List<UserModel> GetAll(Expression<Func<UserModel, bool>> filter = null);
        UserModel GetById(Guid id);
        Task InsertAsync(CreateUserModel model);
        Task UpdateAsync(UserModel model);
    }
}