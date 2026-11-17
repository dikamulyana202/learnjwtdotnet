using AuthJWT.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthJWT.Data
{
   public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
   {
      public DbSet<User> Users { get; set; }
   }
}