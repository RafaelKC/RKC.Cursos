using Microsoft.EntityFrameworkCore;

namespace RKC.Cursos.Context
{
    public class CursosContext : DbContext
    {
        public CursosContext(DbContextOptions<CursosContext> options) : base(options)
        {
            Database.Migrate();
        }

    }
}