using ThriftBooks.Data.Entities;
using ThriftBooks.Data.Repositories.Interfaces;

namespace ThriftBooks.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ThriftBooksContext context) : base(context)
        {

        }
    }
}
