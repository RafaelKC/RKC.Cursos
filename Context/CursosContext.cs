using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Users;

namespace RKC.Cursos.Context
{
    public class CursosContext : DbContext
    {
        public CursosContext(DbContextOptions<CursosContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<User> Users { get; set; }

    }
}