using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Books;
using ThriftBooks.Business.Services.Interfaces;
using ThriftBooks.Infrastructure;

namespace ThriftBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public IActionResult Get(Guid id)
        {
            var result = _bookService.GetById(id);

            if (result == null)
            {
                return NotFound("Object with the provided id does not exist");
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> Post(CreateBookModel model)
        {
            await _bookService.InsertAsync(model);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> Put(BookModel model)
        {
            var user = _bookService.GetById(model.Id);

            if (user == null)
            {
                return NotFound("Object with the provided id does not exist");
            }

            await _bookService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = UserRoleConstants.Admin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = _bookService.GetById(id);

            if (result == null)
            {
                return NotFound("Object with the provided id does not exist");
            }

            await _bookService.DeleteAsync(id);

            return Ok();
        }
    }
}
