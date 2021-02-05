using ThriftBooks.Data.Entities;
using ThriftBooks.Data.Repositories.Interfaces;

namespace ThriftBooks.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ThriftBooksContext context) : base(context)
        {

        }
    }
}
