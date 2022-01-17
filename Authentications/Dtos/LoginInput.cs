using System.ComponentModel.DataAnnotations;

namespace RKC.Cursos.Authentications.Dtos
{
    public class LoginInput
    {
        [Required(AllowEmptyStrings = false)]
        public string EmailOrUserName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}