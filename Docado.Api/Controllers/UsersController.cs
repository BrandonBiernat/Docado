using Docado.Api.Controllers.ViewModels;
using Docado.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Docado.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase {
    [HttpGet]
    public async Task<IActionResult> GetAllUsers() {
        try {
            IEnumerable<IUserEntity> entities = await 
                userService.GetAll();
            IEnumerable<UserViewModel> models = entities
                .Select(e => new UserViewModel(e))
                .ToList();
            return Ok(models);
        }
        catch(Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUsersById(string id) {
        try {
            IUserEntity entity = await
                userService.GetById(id);
            UserViewModel model = new(entity);
            return Ok(model);
        }
        catch(Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }
}