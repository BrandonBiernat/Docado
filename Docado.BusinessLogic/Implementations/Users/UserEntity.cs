using Docado.BusinessLogic.Interfaces;
using Docado.DataAccess.Implementations.Users;

namespace Docado.BusinessLogic.Implementations.Users;

public class UserEntity(UserRecord record) : IUserEntity {
    public string Id { get; set; } = record.Id;
    public string Email { get; set; } = record?.Email ?? string.Empty;
    public string FirstName { get; set; } = record.FirstName;
    public string LastName { get; set; } = record.LastName;
}

public class UserEntityProperties : IUserEntityProperties {
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}