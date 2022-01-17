using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RKC.Cursos.Context;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Dtos;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Services
{
    public class UserRepositoryService : IUserRepositoryService
    {
        public readonly CursosContext _context;

        public UserRepositoryService(CursosContext context)
        {
            _context = context;
        }

        public async Task<List<UserOutput>> GetList(UserGetListInput filterInput)
        {
            var query = _context.Users.AsQueryable();

            if (filterInput.FilterByUserRole.HasValue)
            {
                query = query.Where(user => user.Role == filterInput.FilterByUserRole.Value);
            }

            if (string.IsNullOrEmpty(filterInput.FilterByUserName))
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
            userInput.IsInactive = false;

            var newUser = new User(userInput);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return UserRepositoryResult.Ok;
        }

        public async Task<UserRepositoryResult> Update(Guid userId, IUser userInput)
        {
            var userCreated = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            if (userCreated == null)
            {
                return UserRepositoryResult.NotFound;
            }
            
            userCreated.Update(userInput);
            await _context.SaveChangesAsync();
            return UserRepositoryResult.Ok;
        }

        public async Task<UserOutput> Get(Guid userId)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            return user != null ? new UserOutput(user) : null;
        }
    }
}