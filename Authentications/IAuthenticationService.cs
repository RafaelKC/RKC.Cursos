using RKC.Cursos.Users;

namespace RKC.Cursos.Authentications
{
    public interface  IAuthenticationService
    {
        public string GenerateToken(User user);
    }
}