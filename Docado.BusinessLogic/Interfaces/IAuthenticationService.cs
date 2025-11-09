using Microsoft.AspNetCore.Identity;

namespace Docado.BusinessLogic.Interfaces;

public interface IAuthenticationService {
    Task<IdentityResult> Register(string email, string password, string firstName, string lastName);
    Task<IdentityResult> Login(string email, string password, bool rememberMe);
    Task<IdentityResult> Logout();
    Task<IdentityResult> ConfirmEmail(string userId, string token);
    Task<IdentityResult> ResendEmailConfirmation(string email);
    Task<IdentityResult> ResetPassword(string email);
    Task<IdentityResult> ResetPasswordConfirm(string email, string token, string password);
}