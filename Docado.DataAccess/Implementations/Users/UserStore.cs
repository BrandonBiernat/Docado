using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Docado.DataAccess.Implementations.Users;

public class UserStore(IUserRepository userRepository) : IUserPasswordStore<UserRecord>, IUserEmailStore<UserRecord> {
    public void Dispose() { }

    public async Task<string> GetUserIdAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        user.Id;

    public async Task<string?> GetUserNameAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        user.UserName;

    public async Task SetUserNameAsync(
        UserRecord user,
        string? userName,
        CancellationToken cancellationToken) {
        user.UserName = userName;
        await userRepository.Upsert(user);
    }

    public async Task<string?> GetNormalizedUserNameAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        user.NormalizedUserName;

    public async Task SetNormalizedUserNameAsync(
        UserRecord user,
        string? normalizedName,
        CancellationToken cancellationToken) {
        user.NormalizedUserName = normalizedName;
        await userRepository.Upsert(user);
    }

    public async Task<IdentityResult> CreateAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        await userRepository.Upsert(user);

    public async Task<IdentityResult> UpdateAsync(
        UserRecord user, 
        CancellationToken cancellationToken) =>
        await userRepository.Upsert(user);

    public async Task<IdentityResult> DeleteAsync(
        UserRecord user, 
        CancellationToken cancellationToken) =>
        await userRepository.Delete(user);

    public async Task<UserRecord?> FindByIdAsync(
        string userId,
        CancellationToken cancellationToken) =>
        await userRepository.GetById(userId);

    public async Task<UserRecord?> FindByNameAsync(
        string normalizedUserName,
        CancellationToken cancellationToken) =>
        await userRepository.GetByUsername(normalizedUserName);

    public async Task SetPasswordHashAsync(
        UserRecord user, 
        string? passwordHash, 
        CancellationToken cancellationToken) {
        user.PasswordHash = passwordHash;
        await userRepository.Upsert(user);
    }

    public async Task<string?> GetPasswordHashAsync(
        UserRecord user, 
        CancellationToken cancellationToken) =>
        user.PasswordHash;

    public async Task<bool> HasPasswordAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        true;

    public async Task SetEmailAsync(
        UserRecord user,
        string? email,
        CancellationToken cancellationToken) {
        user.Email = email;
        await userRepository.Upsert(user);
    }

    public async Task<string?> GetEmailAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        user.Email;

    public async Task<bool> GetEmailConfirmedAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        user.EmailConfirmed;

    public async Task SetEmailConfirmedAsync(
        UserRecord user,
        bool confirmed,
        CancellationToken cancellationToken) {
        user.EmailConfirmed = confirmed;
        await userRepository.Upsert(user);
    }

    public async Task<UserRecord?> FindByEmailAsync(
        string normalizedEmail,
        CancellationToken cancellationToken) =>
        await userRepository.GetByEmail(normalizedEmail);

    public async Task<string?> GetNormalizedEmailAsync(
        UserRecord user,
        CancellationToken cancellationToken) =>
        user.NormalizedEmail;

    public Task SetNormalizedEmailAsync(
        UserRecord user,
        string? normalizedEmail,
        CancellationToken cancellationToken) {
        user.NormalizedEmail = normalizedEmail;
        return userRepository.Upsert(user);
    }
}