using System;
using System.Threading.Tasks;
using RKC.Cursos.Authentications.Enums;

namespace RKC.Cursos.Authentications.Services
{
    public interface ICredentialRepositoryService
    {
        public Task<CredentialRepositoryResult> Create(Credential credentialInput);
        public Task<Credential> GetByUserId(Guid userId);
        public Task<CredentialRepositoryResult> Update(Guid userId, Credential credentialInput);
        public string EncryptPassword(string password);
    }
}