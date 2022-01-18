using System;
using System.ComponentModel.DataAnnotations;

namespace RKC.Cursos.Authentications.Abstractions
{
    public interface ICredential
    {
        public Guid UserId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}