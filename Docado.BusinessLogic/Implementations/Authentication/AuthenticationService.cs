using System.Text.RegularExpressions;
using Docado.BusinessLogic.Interfaces;
using Docado.DataAccess.Implementations.Users;
using Microsoft.AspNetCore.Identity;

namespace Docado.BusinessLogic.Implementations.Authentication;

public class AuthenticationService(
    UserManager<UserRecord> userManager,
    SignInManager<UserRecord> signInManager) : IAuthenticationService {
    public async Task<IdentityResult> Register(
        string email, 
        string password,
        string firstName,
        string lastName) {
        string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        if (!Regex.IsMatch(email, pattern)) {
            return IdentityResult.Failed(new IdentityError {
                Code = "Email",
                Description = "Invalid email address",
            });
        }

        UserRecord user = UserRecord.Build(
            firstName: firstName,
            lastName: lastName,
            email: email);
        IdentityResult result = await 
            userManager.CreateAsync(user, password);

        if (!result.Succeeded) {
            return IdentityResult.Failed(result.Errors.ToArray());
        }
        
        string token = await 
            userManager.GenerateEmailConfirmationTokenAsync(user);
        
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> Login(
        string email, 
        string password,
        bool rememberMe) {
        UserRecord? user = await 
            userManager.FindByEmailAsync(email);

        if (user is null) {
            return IdentityResult.Failed(new IdentityError {
                Code = "Email",
                Description = "Email is incorrect",
            });
        }
        
        bool passwordResult = await 
            userManager.CheckPasswordAsync(user, password);
        if (!passwordResult) {
            return IdentityResult.Failed(new IdentityError {
                Code = "Password",
                Description = "Password is incorrect",
            });
        }
        
        bool isEmailConfirmed = await 
            userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed) {
            return IdentityResult.Failed(new IdentityError {
                Code = "Email",
                Description = "Email has not been confirmed",
            });
        }

        SignInResult signInResult = await
            signInManager.PasswordSignInAsync(
                user: user,
                password: password,
                isPersistent: rememberMe,
                lockoutOnFailure: true);

        IdentityResult result = signInResult.Succeeded
            ? IdentityResult.Success
            : IdentityResult.Failed();
        
        return result;
    }

    public async Task<IdentityResult> Logout() {
        await signInManager.SignOutAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ConfirmEmail(
        string userId, 
        string token) {
        UserRecord? user = await 
            userManager.FindByIdAsync(userId);

        if (user is null) {
            throw new Exception("User not found");
        }
        
        IdentityResult result = await 
            userManager.ConfirmEmailAsync(user, token);
        
        return result;
    }

    public Task<IdentityResult> ResendEmailConfirmation(string email) {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> ResetPassword(string email) {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> ResetPasswordConfirm(
        string email, 
        string token, 
        string password) {
        UserRecord? user = await
            userManager.FindByEmailAsync(email);

        if (user is null) {
            throw new Exception("Invalid email address");
        }

        IdentityResult result = await
            userManager.ResetPasswordAsync(user, token, password);
        
        return result;
    }
}