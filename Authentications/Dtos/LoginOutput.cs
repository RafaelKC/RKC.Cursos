using RKC.Cursos.Users.Dtos;

namespace RKC.Cursos.Authentications.Dtos
{
    public class LoginOutput
    {
        public string AccessToken { get; set; }
        public UserOutput User { get; set; }
    }
}