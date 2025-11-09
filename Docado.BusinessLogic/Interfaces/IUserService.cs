namespace Docado.BusinessLogic.Interfaces;

public interface IUserService {
    Task<IEnumerable<IUserEntity>> GetAll();
    Task<IEnumerable<IUserEntity>> GetByEmail(IEnumerable<string> emails);
    Task<IEnumerable<IUserEntity>> GetById(IEnumerable<string> ids);

    async Task<IUserEntity?> GetByEmail(string email) {
        IEnumerable<IUserEntity> entities = await GetByEmail([email]);
        IUserEntity entity = 
            entities.FirstOrDefault() 
            ?? throw new Exception("User not found");
        return entity;
    }
    async Task<IUserEntity> GetById(string id) {
        IEnumerable<IUserEntity> entities = await GetById([id]);
        IUserEntity entity = 
            entities.FirstOrDefault() 
            ?? throw new Exception("User not found");
        return entity;
    }

    Task<IServiceResult> UpdateUser(
        string userId,
        Action<IUserEntityProperties> configure);
    
    Task<IServiceResult> DeleteUser(string userid);
}

public interface IUserEntity : IUserEntityProperties {
    string Id  { get; set; }
}

public interface IUserEntityProperties {
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
}