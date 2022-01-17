using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RKC.Cursos.Authentications.Dtos;
using RKC.Cursos.Authentications.Services;
using RKC.Cursos.Users.Services;

namespace RKC.Cursos.Authentications.Controllers
{
    [Route("cursos/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICredentialRepositoryService _credentialRepository;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            IUserService userService,
            ICredentialRepositoryService credentialRepository,
            IAuthenticationService authenticationService)
        {
            _userService = userService;
            _credentialRepository = credentialRepository;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginOutput>> Login([FromBody] LoginInput loginInput)
        {
            if (string.IsNullOrEmpty(loginInput.Password) || string.IsNullOrEmpty(loginInput.EmailOrUserName)) return BadRequest();
            
            loginInput.Password = _credentialRepository.EncryptPassword(loginInput.Password);

            var user = await _userService.GetByEmailOrUserName(loginInput.EmailOrUserName);
            if (user == null) return NotFound();

            var credential = await _credentialRepository.GetByUserId(user.Id);
            if (credential == null) return UnprocessableEntity("Not found credential to user");

            var loginValid = credential.Email == user.Email && credential.Password == loginInput.Password;

            if (!loginValid) return UnprocessableEntity("Invalid password");

            var accessToken = _authenticationService.GenerateToken(user);

            return Ok(new LoginOutput
            {
                User = user,
                AccessToken = accessToken
            });
        }
    }
}