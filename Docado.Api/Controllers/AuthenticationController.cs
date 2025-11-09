using Docado.BusinessLogic.Interfaces;
using ExtensionMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Docado.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase {
    public class RegisterRequestModel {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestModel request) {
        try {
            (await authenticationService
                .Register(
                    email: request.email,
                    firstName: request.firstName,
                    lastName: request.lastName,
                    password: request.password))
                .VerifyIdentityResult();
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }
    
    public class LoginRequestModel {
        public string email { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestModel request) {
        try {
            (await authenticationService
                .Login(
                    email: request.email,
                    password: request.password,
                    rememberMe: request.rememberMe))
                .VerifyIdentityResult();
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }
    
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout() {
        try {
            (await authenticationService
                .Logout())
                .VerifyIdentityResult();
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh(
        [FromQuery] string refreshToken ) {
        try {
            
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }
    
    [HttpGet]
    [Route("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] string userId,
        [FromQuery] string token) {
        try {
            IdentityResult result = await authenticationService
                .ConfirmEmail(
                    userId: userId,
                    token: token);
            if (!result.Succeeded) {
                return BadRequest(result.Errors);
            }
            return Redirect("");
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }

    public class ResendConfirmationEmailRequestModel {
        public string email { get; set; }
    }
    
    [HttpPost]
    [Route("resendConfirmationEmail")]
    public async Task<IActionResult> ResendConfirmationEmail(
        [FromBody]  ResendConfirmationEmailRequestModel request) {
        try {
            
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }
    
    public class ForgotPasswordRequestModel {
        public string email { get; set; }
    }
    
    [HttpPost]
    [Route("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(
        [FromBody] ForgotPasswordRequestModel request) {
        try {
            
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }

    public class ResetPasswordRequestModel {
        public string email { get; set; }
        public string resetCode  { get; set; }
        public string newPassword { get; set; }
    }
    
    [HttpPost]
    [Route("resetPassword")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordRequestModel request) {
        try {
            return Ok();
        }
        catch (Exception ex) {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: ex.Message);
        }
    }
}