using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Dtos;
using RKC.Cursos.Users.Enums;
using RKC.Cursos.Users.Services;

namespace RKC.Cursos.Users.Controllers
{
    [Route("cursos/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _repositoryService;

        public UserController(IUserService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<ActionResult> Create([FromBody] UserInput userInput)
        {
            if (string.IsNullOrEmpty(userInput.Email)
                || string.IsNullOrEmpty(userInput.Password)
                || string.IsNullOrEmpty(userInput.FirstName)
                || string.IsNullOrEmpty(userInput.LastName)) return BadRequest("Input fail validation");
            
            return Ok(await _repositoryService.Create(userInput));
        }
        
        [HttpGet("{userId:guid}")]
        [Authorize]
        public async Task<ActionResult<UserOutput>> Get([FromRoute] Guid userId)
        {
            var user = await _repositoryService.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserOutput>>> GetList([FromQuery] UserGetListInput filterInput)
        {
            return Ok(await _repositoryService.GetList(filterInput));
        }
        
        [HttpPut("{userId:guid}")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<ActionResult<UserOutput>> Update([FromRoute] Guid userId, [FromBody] UserInput userInput)
        {
            if (string.IsNullOrEmpty(userInput.Email)
                || string.IsNullOrEmpty(userInput.FirstName)
                || string.IsNullOrEmpty(userInput.LastName)) return BadRequest("Input fail validation");

            var userResult = await _repositoryService.Update(userId, userInput);
            return userResult switch
            {
                UserRepositoryResult.NotFound => NotFound(),
                UserRepositoryResult.CantUpdateSystemAdmin => BadRequest("Can't update System Admin"),
                _ => Ok()
            };
        }
    }
}