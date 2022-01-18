using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RKC.Cursos.Aulas.Abstractions;
using RKC.Cursos.Aulas.Dtos;
using RKC.Cursos.Aulas.Enums;

namespace RKC.Cursos.Aulas.Services
{
    public interface IAulaRepositoryService
    {
        public Task<AulaRepositoryResult> Create(Guid moduloId, AulaInput aulaInput);
        public Task<AulaOutput> Get(Guid moduloId, Guid aulaId);
        public Task<List<AulaOutput>> GetList(Guid moduloId, string nameFilter);
        public Task<AulaRepositoryResult> Update(Guid moduloId, Guid aulaId, IAula aulaInput);
        public Task<AulaRepositoryResult> Update(Guid moduloId, Guid aulaId);
    }
}