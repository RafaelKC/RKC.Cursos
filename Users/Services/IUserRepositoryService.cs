﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Dtos;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Services
{
    public interface IUserRepositoryService
    {
        public Task<List<UserOutput>> GetList(UserGetListInput filterInput);
        public Task<UserRepositoryResult> Create(UserInput userInput);
        public Task<UserRepositoryResult> Update(Guid userId, IUser userInput);
        public Task<UserOutput> Get(Guid userId);
    }
}