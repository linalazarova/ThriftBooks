using System;

namespace ThriftBooks.Business.Models.Books
{
    public class BookModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
