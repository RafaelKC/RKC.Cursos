using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Authentications.Enums;
using RKC.Cursos.Context;

namespace RKC.Cursos.Authentications.Services
{
    public class CredentialRepositoryService : ICredentialRepositoryService
    {
        private readonly CursosContext _context;

        public CredentialRepositoryService(CursosContext context)
        {
            _context = context;
        }

        public async Task<CredentialRepositoryResult> Create(Credential credentialInput)
        {
            var credentialAlredyCreated = await _context.Credentials.AnyAsync(credential => credential.UserId == credentialInput.UserId
                || credential.Id == credentialInput.Id || credential.Email == credentialInput.Email);
            if (credentialAlredyCreated) return CredentialRepositoryResult.CredentialAlredyCreated;

            credentialInput.Password = EncryptPassword(credentialInput.Password);
            await _context.Credentials.AddAsync(credentialInput);
            await _context.SaveChangesAsync();

            return CredentialRepositoryResult.Ok;
        }

        public async Task<Credential> GetByUserId(Guid userId)
        {
            return await _context.Credentials.FirstOrDefaultAsync(credential => credential.UserId == userId);
        }

        public async Task<CredentialRepositoryResult> Update(Guid userId, Credential credentialInput)
        {
            var credentialCreated = await _context.Credentials.FirstOrDefaultAsync(credential => credential.UserId == userId);
            if (credentialCreated == null) return CredentialRepositoryResult.NotFound;

            credentialInput.Password = EncryptPassword(credentialInput.Password);
            
            credentialCreated.Update(credentialInput);
            _context.Credentials.Update(credentialCreated);
            await _context.SaveChangesAsync();
            
            return CredentialRepositoryResult.Ok;
        }
        
        public string EncryptPassword(string password)
        {
            var sha = new SHA512CryptoServiceProvider();
            var passwordBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            var stringBuilder = new StringBuilder();
            foreach (var bte in passwordBytes)
            {
                stringBuilder.Append(bte.ToString("X2"));
            }
            
            return stringBuilder.ToString();
        }
    }
}