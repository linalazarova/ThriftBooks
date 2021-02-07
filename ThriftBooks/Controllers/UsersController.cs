using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Users;
using ThriftBooks.Business.Services.Interfaces;
using ThriftBooks.Infrastructure;

namespace ThriftBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;

        public UsersController(IUserService userService, IBookService bookService)
        {
            _userService = userService;
            _bookService = bookService;
        }

        [HttpGet]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public IActionResult Get(Guid id)
        {
            var result = _userService.GetById(id);

            if (result == null)
            {
                return NotFound("Object with the provided id does not exist");
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> Post(CreateUserModel model)
        {
            if (_userService.DoesUsernameExist(model.Username))
            {
                return BadRequest("Username already exists");
            }

            await _userService.InsertAsync(model);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> Put(EditUserModel model)
        {
            var user = _userService.GetById(model.Id);

            if (user == null)
            {
                return BadRequest("Object with the provided id does not exist");
            }

            if (model.Username != user.Username && _userService.DoesUsernameExist(model.Username))
            {
                return BadRequest("Username already exists");
            }

            await _userService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = _userService.GetById(id);

            if (result == null)
            {
                return BadRequest("Object with the provided id does not exist");
            }

            await _userService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("{userId}/books")]
        [Authorize]
        public IActionResult GetUserBooks(Guid userId)
        {
            var user = _userService.GetUserWithBooks(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user.Books);
        }

        [HttpPost("{userId}/books/{bookId}")]
        [Authorize]
        public async Task<IActionResult> BuyBook(Guid userId, Guid bookId)
        {
            var user = _userService.GetUserWithBooks(userId);
            var book = _bookService.GetById(bookId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            if (book.Quantity <= 0)
            {
                return BadRequest("There aren't enough copies of this book in stock.");
            }

            if (user.Books.Any(b => b.Id == bookId))
            {
                return BadRequest("You can't buy more than one copy of this book.");
            }

            await _userService.BuyBook(userId, bookId);

            return Ok();
        }
        
    }
}
