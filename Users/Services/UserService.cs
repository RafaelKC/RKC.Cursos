using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Authentications;
using RKC.Cursos.Authentications.Enums;
using RKC.Cursos.Authentications.Services;
using RKC.Cursos.Context;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Dtos;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Services
{
    public class UserService : IUserService
    {
        private readonly CursosContext _context;
        private readonly ICredentialRepositoryService _credentialRepository;

        public UserService(CursosContext context, ICredentialRepositoryService credentialRepository)
        {
            _context = context;
            _credentialRepository = credentialRepository;
        }

        public async Task<List<UserOutput>> GetList(UserGetListInput filterInput)
        {
            var query = _context.Users.AsNoTracking().AsQueryable();

            if (filterInput.FilterByUserRole.HasValue)
            {
                query = query.Where(user => user.Role == filterInput.FilterByUserRole.Value);
            }

            if (!string.IsNullOrEmpty(filterInput.FilterByUserName))
            {
                query = query.Where(user => user.UserName.ToLower().Contains(filterInput.FilterByUserName.ToLower()));
            }
            
            if (!filterInput.GetInactivesToo)
            {
                query = query.Where(user => user.IsInactive == false);
            }

            return await query.OrderBy(user => user.FirstName).Select(user => new UserOutput(user)).ToListAsync();
        }

        public async Task<UserRepositoryResult> Create(UserInput userInput)
        {
            var userAlreadyCreated = await _context.Users.AnyAsync(user =>
                user.Id == userInput.Id || user.Email == userInput.Email);
            if (userAlreadyCreated) return UserRepositoryResult.UserAlredyCreated;
            
            userInput.IsInactive = false;
            if (string.IsNullOrEmpty(userInput.UserName))
                userInput.UserName = $"{userInput.FirstName} {userInput.LastName}";
            
            
            var newUser = new User(userInput);
            await _context.Users.AddAsync(newUser);

            var credential = new Credential
            {
                Id = Guid.NewGuid(),
                Email = newUser.Email,
                Password = userInput.Password,
                UserId = newUser.Id
            };

            var credentialResult = await _credentialRepository.Create(credential);

            if (credentialResult == CredentialRepositoryResult.CredentialAlredyCreated)
                return UserRepositoryResult.UserAlredyCreated;
            
            await _context.SaveChangesAsync();
            return UserRepositoryResult.Ok;
        }

        public async Task<UserRepositoryResult> Update(Guid userId, UserInput userInput)
        {
            if (userId == Guid.Parse("35265aa9-ca64-4923-b191-5a0d8e1c5c28"))
                return UserRepositoryResult.CantUpdateSystemAdmin;
            
            var userCreated = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if (userCreated == null) return UserRepositoryResult.NotFound;

            if (string.IsNullOrEmpty(userInput.UserName))
            {
                userInput.UserName = userCreated.UserName;
            }
            userCreated.Update(userInput);
            _context.Users.Update(userCreated);

            await _context.SaveChangesAsync();
            return UserRepositoryResult.Ok;
        }

        public async Task<UserOutput> Get(Guid userId)
        {
            var user =  await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == userId);
            return user != null ? new UserOutput(user) : null;
        }

        public async Task<UserOutput> GetByEmailOrUserName(string emailOrUserName)
        {
            var user =  await _context.Users
                .AsNoTracking().FirstOrDefaultAsync(user => 
                user.Email == emailOrUserName || user.UserName == emailOrUserName);
            return user != null ? new UserOutput(user) : null;
        }
    }
}