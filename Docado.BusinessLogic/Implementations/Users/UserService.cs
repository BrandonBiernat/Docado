using System.Text.RegularExpressions;
using Docado.BusinessLogic.Interfaces;
using Docado.BusinessLogic.ReturnTypes;
using Docado.DataAccess.Implementations.Users;
using Docado.DataAccess.Interfaces;
using ExtensionMethods;
using Microsoft.AspNetCore.Identity;

namespace Docado.BusinessLogic.Implementations.Users;

public class UserService(
    IUserRepository userRepository,
    UserManager<UserRecord> userManager) : IUserService {
    public async Task<IEnumerable<IUserEntity>> GetAll() {
        IEnumerable<UserRecord> records = await 
            userRepository.GetAll();
        IEnumerable<UserEntity> entities =
            records.Select(record => new UserEntity(record));
        return entities;
    }

    public async Task<IEnumerable<IUserEntity>> GetByEmail(
        IEnumerable<string> emails) {
        IEnumerable<UserRecord> records = await 
            userRepository.GetByEmail(emails);
        IEnumerable<UserEntity> entities =
            records.Select(record => new UserEntity(record));
        return entities;
    }

    public async Task<IEnumerable<IUserEntity>> GetById(
        IEnumerable<string> ids) {
        IEnumerable<UserRecord> records = await 
            userRepository.GetById(ids);
        IEnumerable<UserEntity> entities =
            records.Select(record => new UserEntity(record));
        return entities;
    }

    public async Task<IServiceResult> UpdateUser(
        string userId, 
        Action<IUserEntityProperties> configure) {
        IUserEntityProperties properties = new UserEntityProperties();
        configure(properties);
        
        UserRecord user = await 
            userRepository.GetById(userId) ??
                          throw new Exception("User not found");

        UserRecord userToUpdate = user.Clone();
        userToUpdate.FirstName = properties.FirstName;
        userToUpdate.LastName = properties.LastName;
        userToUpdate.Email = properties.Email;
        
        (await userRepository
            .Upsert(userToUpdate))
            .VerifyIdentityResult();

        return new ServiceResult();
    }

    public async Task<IServiceResult> DeleteUser(
        string userid) {
        UserRecord record = await 
            userRepository.GetById(userid) ??
                            throw new Exception("User not found");
        (await userRepository
            .Delete(record))
            .VerifyIdentityResult();
        
        return new ServiceResult();
    }
}