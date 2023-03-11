using Chat.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure;

public class AuthenticationContext : IdentityDbContext<User>
{
    public const string DEFAULT_SCHEMA = "generic";

    public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }
}
