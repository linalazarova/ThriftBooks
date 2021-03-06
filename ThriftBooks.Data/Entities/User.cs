﻿using System.Collections.Generic;
using ThriftBooks.Data.Enums;

namespace ThriftBooks.Data.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public UserRoles Role { get; set; }
        public string Password { get; set; }
        public List<BookUser> BookUsers { get; set; } = new List<BookUser>();
    }
}
