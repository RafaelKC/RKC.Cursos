using RKC.Cursos.Users.Dtos;

namespace RKC.Cursos.Authentications.Services
{
    public interface  IAuthenticationService
    {
        public string GenerateToken(UserOutput user);
    }
}