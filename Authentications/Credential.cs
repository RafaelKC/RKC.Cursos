using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RKC.Cursos.Authentications.Abstractions;

namespace RKC.Cursos.Authentications
{
    [Table("Credentials")]
    public class Credential : ICredential
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        public void Update(ICredential credentialInput)
        {
            Email = credentialInput.Email;
            Password = credentialInput.Password;
        }
    }
}