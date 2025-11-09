using Docado.DataAccess.DB;
using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Docado.DataAccess.Implementations.Users;

public class UserRepository(DocadoDbContext context) : IUserRepository {
    public async Task<IEnumerable<UserRecord>> GetAll() {
        IEnumerable<UserRecord> records = await 
            context.Users.ToListAsync();
        return records;
    }

    public async Task<IEnumerable<UserRecord>> GetById(
        IEnumerable<string> ids) {
        IEnumerable<UserRecord> records = await 
            context.Users.Where(r => 
                ids.Contains(r.Id)).ToListAsync();
        return records;
    }

    public async Task<IEnumerable<UserRecord>> GetByEmail(
        IEnumerable<string> emails) {
        IEnumerable<UserRecord> records = await
            context.Users.Where(r => 
                emails.Contains(r.Email)).ToListAsync();
        return records;
    }

    public async Task<IEnumerable<UserRecord>> GetByUsername(
        IEnumerable<string> usernames) {
        IEnumerable<UserRecord> records = await
            context.Users.Where(r => 
                usernames.Contains(r.NormalizedUserName)).ToListAsync();
        return records;
    }

    public async Task<UserRecord?> GetByEmailAndPassword(
        string email, 
        string password) {
        UserRecord record = await 
            context.Users.FirstOrDefaultAsync(r => 
                r.Email == email && r.PasswordHash == password) ?? 
                            throw new Exception("User not found");
        return record;
    }

    public async Task<IdentityResult> Upsert(
        IEnumerable<UserRecord> userRecords) {
        try {
            await context.BulkMergeAsync(userRecords);
            return IdentityResult.Success;
        }
        catch (Exception e) {
            return IdentityResult.Failed();
        }
    }

    public async Task<IdentityResult> Delete(
        IEnumerable<UserRecord> userRecords) {
        try {
            await context.BulkDeleteAsync(userRecords);
            return IdentityResult.Success;
        }
        catch (Exception e) {
            return IdentityResult.Failed();
        }
    }
}