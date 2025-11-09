using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Docado.DataAccess.Implementations.Users;

public class UserRecord : IdentityUser {
    public UserRecord() { }
    private UserRecord(
        string firstName,
        string lastName) : base() {
        FirstName = firstName;
        LastName = lastName;
        CreationDate = DateTime.Now;
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    
    public UserRecord Clone() =>
        MemberwiseClone() as UserRecord ??
        throw new Exception("Failed to clone UserRecord");

    public static UserRecord Build(
        string firstName,
        string lastName) =>
        new(firstName, lastName);
}