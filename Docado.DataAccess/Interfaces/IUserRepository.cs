using Docado.DataAccess.Implementations.Users;
using Microsoft.AspNetCore.Identity;

namespace Docado.DataAccess.Interfaces;

public interface IUserRepository {
    Task<IEnumerable<UserRecord>> GetAll();
    Task<IEnumerable<UserRecord>> GetById(IEnumerable<string> ids);
    Task<IEnumerable<UserRecord>> GetByEmail(IEnumerable<string> emails);
    Task<IEnumerable<UserRecord>> GetByUsername(IEnumerable<string> usernames);
    
    async Task<UserRecord?> GetById(string id) {
        IEnumerable<UserRecord> records = await GetById([id]);
        return records.FirstOrDefault();
    }
    async Task<UserRecord?> GetByEmail(string email)
    {
        IEnumerable<UserRecord> records = await GetByEmail([email]);
        return records.FirstOrDefault();
    }
    async Task<UserRecord?> GetByUsername(string username)
    {
        IEnumerable<UserRecord> records = await GetByUsername([username]);
        return records.FirstOrDefault();
    }
    
    Task<UserRecord?> GetByEmailAndPassword(string email, string password);
    
    Task<IdentityResult>  Upsert(IEnumerable<UserRecord> userRecords);
    Task<IdentityResult>  Upsert(UserRecord userRecord) => Upsert([userRecord]);
    
    Task<IdentityResult>  Delete(IEnumerable<UserRecord> userRecords);
    Task<IdentityResult>  Delete(UserRecord userRecord) =>  Delete([userRecord]);
}