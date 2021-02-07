using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThriftBooks.Data.Entities;
using ThriftBooks.Data.Repositories;

namespace ThriftBooks.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task BuyBook(Guid userId, Guid bookId);
        User GetUserWithBooks(Guid id);
        IEnumerable<User> GetAllUsersWithBooks(Expression<Func<User, bool>> filter = null);
        User GetUserByUsername(string username);
    }
}
