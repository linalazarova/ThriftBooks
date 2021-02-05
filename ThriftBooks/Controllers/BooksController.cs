using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Books;
using ThriftBooks.Business.Services.Interfaces;

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
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Post(CreateBookModel model)
        {
            await _bookService.InsertAsync(model);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(BookModel model)
        {
            var user = _bookService.GetById(model.Id);

            if (user == null)
            {
                return BadRequest("Object with the provided id does not exist");
            }

            await _bookService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = _bookService.GetById(id);

            if (result == null)
            {
                return BadRequest("Object with the provided id does not exist");
            }

            await _bookService.DeleteAsync(id);

            return Ok();
        }
    }
}
