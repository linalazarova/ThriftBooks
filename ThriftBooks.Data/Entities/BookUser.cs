using System;
using System.Collections.Generic;
using System.Text;

namespace ThriftBooks.Data.Entities
{
    public class BookUser
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
