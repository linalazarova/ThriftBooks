using System;
using System.ComponentModel.DataAnnotations;

namespace ThriftBooks.Business.Models.Users
{
    public class UserModel : BaseModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
