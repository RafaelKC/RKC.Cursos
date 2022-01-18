using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RKC.Cursos.Modulos.Abstractions;
using RKC.Cursos.Modulos.Dtos;
using RKC.Cursos.Modulos.Enums;

namespace RKC.Cursos.Modulos.Services
{
    public interface IModuloRepositoryService
    {
        public Task<ModuloRepositoryResult> Create(ModuloInput moduloInput);
        public Task<ModuloOutput> Get(Guid moduloId);
        public Task<List<ModuloOutput>> GetList(string nomeFilter);
        public Task<ModuloRepositoryResult> Update(Guid moduloId, IModulo moduloInput);
        public Task<ModuloRepositoryResult> Delete(Guid moduloId);
    }
}