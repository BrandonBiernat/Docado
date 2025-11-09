using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Docado.DataAccess.Implementations.Users;

public class UserRecord : IdentityUser {
    public UserRecord() { }
    private UserRecord(
        string firstName,
        string lastName,
        string email) : base() {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CreationDate = DateTime.Now;
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set;  }
    public DateTime CreationDate { get; set; }
    
    public UserRecord Clone() =>
        MemberwiseClone() as UserRecord ??
        throw new Exception("Failed to clone UserRecord");

    public static UserRecord Build(
        string firstName,
        string lastName,
        string email) =>
        new(firstName, lastName, email);
}