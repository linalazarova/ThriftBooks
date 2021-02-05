using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Users;
using ThriftBooks.Business.Services.Interfaces;

namespace ThriftBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Post(CreateUserModel model)
        {
            await _userService.InsertAsync(model);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserModel model)
        {
            var user = _userService.GetById(model.Id);

            if (user == null)
            {
                return BadRequest("Object with the provided id does not exist");
            }

            await _userService.UpdateAsync(model);

            return Ok();
        }

        [HttpDelete]
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
    }
}
