using GameStore.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Identity.Persistence;

public class IdentityContext : IdentityDbContext<UserIdentity, UserRole, int>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
        
    }
}
