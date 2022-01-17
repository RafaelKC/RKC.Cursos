using RKC.Cursos.Users;

namespace RKC.Cursos.Authentications.Services
{
    public interface  IAuthenticationService
    {
        public string GenerateToken(User user);
    }
}