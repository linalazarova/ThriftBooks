using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThriftBooks.Data.Entities;
using ThriftBooks.Data.Repositories.Interfaces;

namespace ThriftBooks.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IBookRepository _bookRepository;

        public UserRepository(ThriftBooksContext context, IBookRepository bookRepository) : base(context)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<User> GetAllUsersWithBooks(Expression<Func<User, bool>> filter = null)
        {
            if (filter != null)
            {
                return DbSet.Where(filter)
                    .Include(u => u.BookUsers)
                    .ThenInclude(bu => bu.Book); ;
            }

            return DbSet.Include(u => u.BookUsers)
                .ThenInclude(bu => bu.Book);
        }

        public User GetUserWithBooks(Guid id)
        {
            var result = DbSet.Include(u => u.BookUsers)
                .ThenInclude(bu => bu.Book)
                .FirstOrDefault(x => x.Id == id);

            return result;
        }

        public async Task BuyBook(Guid userId, Guid bookId)
        {
            var user = GetById(userId);
            var book = _bookRepository.GetById(bookId);
            book.Quantity -= 1;
            var boughtBook = new BookUser { BookId = bookId };
            user.BookUsers.Add(boughtBook);

            await Context.SaveChangesAsync();
        }

        public User GetUserByUsername(string username)
        {
            var result = DbSet.FirstOrDefault(u => u.Username == username);

            return result;
        }


    }
}
