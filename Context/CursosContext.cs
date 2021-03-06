using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Aulas;
using RKC.Cursos.Authentications;
using RKC.Cursos.Modulos;
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
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Aula> Aulas { get; set; }

    }
}