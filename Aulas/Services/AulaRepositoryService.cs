using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Aulas.Abstractions;
using RKC.Cursos.Aulas.Dtos;
using RKC.Cursos.Aulas.Enums;
using RKC.Cursos.Context;

namespace RKC.Cursos.Aulas.Services
{
    public class AulaRepositoryService : IAulaRepositoryService
    {
        private readonly CursosContext _context;

        public AulaRepositoryService(CursosContext context)
        {
            _context = context;
        }

        public async Task<AulaRepositoryResult> Create(Guid moduloId, AulaInput aulaInput)
        {
            var aulaAlredyCreated = await _context.Aulas.AnyAsync(aula => aula.Id == aulaInput.Id);
            if (aulaAlredyCreated) return AulaRepositoryResult.AulaAlredyCreated;

            aulaInput.ModuloId = moduloId; 
            var newAula = new Aula(aulaInput);
            await _context.AddAsync(newAula);
            await _context.SaveChangesAsync();

            return AulaRepositoryResult.Ok;
        }

        public async Task<AulaOutput> Get(Guid moduloId, Guid aulaId)
        {
            var aula = await _context.Aulas
                .AsNoTracking().FirstOrDefaultAsync(aula => aula.Id == aulaId && aula.ModuloId == moduloId);
            return aula != null ? new AulaOutput(aula) : null;
        }

        public async Task<List<AulaOutput>> GetList(List<Guid> modulosIds, string nameFilter)
        {
            var query = _context.Aulas
                .Where(aula => modulosIds.Contains(aula.ModuloId))
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(aula => aula.Nome.ToLower().Contains(nameFilter.ToLower()));
            }

            return await query.OrderBy(aula => aula.Nome).Select(aula => new AulaOutput(aula)).ToListAsync();
        }

        public async Task<AulaRepositoryResult> Update(Guid moduloId, Guid aulaId, IAula aulaInput)
        {
            var aulaCreated = await _context.Aulas.FirstOrDefaultAsync(aula => aula.Id == aulaId && aula.ModuloId == moduloId);
            if (aulaCreated == null) return AulaRepositoryResult.NotFound;
            
            aulaCreated.Update(aulaInput);
            _context.Aulas.Update(aulaCreated);
            await _context.SaveChangesAsync();

            return AulaRepositoryResult.Ok;
        }

        public async Task<AulaRepositoryResult> Update(Guid moduloId, Guid aulaId)
        {
            var aulaCreated = await _context.Aulas.FirstOrDefaultAsync(aula => aula.Id == aulaId && aula.ModuloId == moduloId);
            if (aulaCreated == null) return AulaRepositoryResult.NotFound;
            
            _context.Aulas.Remove(aulaCreated);
            await _context.SaveChangesAsync();

            return AulaRepositoryResult.Ok;
        }
    }
}