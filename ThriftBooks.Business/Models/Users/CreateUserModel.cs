using System.ComponentModel.DataAnnotations;

namespace ThriftBooks.Business.Models.Users
{
    public class CreateUserModel : BaseModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        //public bool IsAdmin { get; set; }
    }
    
    
}
