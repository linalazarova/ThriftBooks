using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThriftBooks.Business.Models.Books;

namespace ThriftBooks.Business.Models.Users
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            Books = new List<BookModel>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        public bool IsAdmin { get; set; }

        public List<BookModel> Books { get; set; }
    }
}
