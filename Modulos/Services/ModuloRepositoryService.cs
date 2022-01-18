using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Context;
using RKC.Cursos.Modulos.Abstractions;
using RKC.Cursos.Modulos.Dtos;
using RKC.Cursos.Modulos.Enums;

namespace RKC.Cursos.Modulos.Services
{
    public class ModuloRepositoryService : IModuloRepositoryService
    {
        private readonly CursosContext _context;

        public ModuloRepositoryService(CursosContext context)
        {
            _context = context;
        }

        public async Task<ModuloRepositoryResult> Create(ModuloInput moduloInput)
        {
            var moduloAlredyCreated = await _context.Modulos.AnyAsync(modulo => modulo.Id == moduloInput.Id);
            if (moduloAlredyCreated) return ModuloRepositoryResult.ModuloAlredyCreated;

            var newModulo = new Modulo(moduloInput);
            await _context.Modulos.AddAsync(newModulo);
            await _context.SaveChangesAsync();

            return ModuloRepositoryResult.Ok;
        }

        public async Task<ModuloOutput> Get(Guid moduloId)
        {
            var modulo = await _context.Modulos
                .AsNoTracking()
                .FirstOrDefaultAsync(modulo => modulo.Id == moduloId);
            return modulo != null ? new ModuloOutput(modulo) : null;
        }

        public async Task<List<ModuloOutput>> GetList(string nomeFilter)
        {
            var query = _context.Modulos.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(nomeFilter))
            {
                query = query.Where(modulo => modulo.Nome.ToLower().Contains(nomeFilter.ToLower()));
            }

            return await query.OrderBy(modulo => modulo.Nome).Select(modulo => new ModuloOutput(modulo)).ToListAsync();
        }

        public async Task<ModuloRepositoryResult> Update(Guid moduloId, IModulo moduloInput)
        {
            var moduloCreated = await _context.Modulos.FirstOrDefaultAsync(modulo => modulo.Id == moduloId);
            if (moduloCreated == null) return ModuloRepositoryResult.NotFound;
            
            moduloCreated.Update(moduloInput);

            _context.Modulos.Update(moduloCreated);
            await _context.SaveChangesAsync();
            return ModuloRepositoryResult.Ok;
        }

        public async Task<ModuloRepositoryResult> Delete(Guid moduloId)
        {
            var moduloCreated = await _context.Modulos.AnyAsync(modulo => modulo.Id == moduloId);
            if (!moduloCreated) return ModuloRepositoryResult.NotFound;

            _context.Remove(moduloId);
            await _context.SaveChangesAsync();
            
            return ModuloRepositoryResult.Ok;
        }
    }
}