using System;
using System.Collections.Generic;
using System.Text;

namespace ThriftBooks.Data.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        //[Min(0)]
        public int Quantity { get; set; }
        public List<BookUser> BookUsers { get; set; } = new List<BookUser>();
    }
}
