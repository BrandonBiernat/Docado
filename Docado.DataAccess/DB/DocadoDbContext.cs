using Docado.DataAccess.Implementations;
using Docado.DataAccess.Implementations.Users;
using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Docado.DataAccess.DB;

public class DocadoDbContext(DbContextOptions<DocadoDbContext> options) : IdentityDbContext<UserRecord>(options) {
    
}