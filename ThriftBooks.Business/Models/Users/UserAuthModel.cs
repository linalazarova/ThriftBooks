using System;
using System.ComponentModel.DataAnnotations;
using ThriftBooks.Data.Enums;

namespace ThriftBooks.Business.Models.Users
{
    public class UserAuthModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRoles Role { get; set; }
    }
}
