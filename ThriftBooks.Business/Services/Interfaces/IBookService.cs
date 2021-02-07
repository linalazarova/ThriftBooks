using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Books;

namespace ThriftBooks.Business.Services.Interfaces
{
    public interface IBookService
    {
        List<BookModel> GetAll(Expression<Func<BookModel, bool>> filter = null);
        BookModel GetById(Guid id);
        Task InsertAsync(CreateBookModel model);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(BookModel model);
    }
}