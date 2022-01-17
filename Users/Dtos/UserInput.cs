using System;
using System.ComponentModel.DataAnnotations;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Dtos
{
    public class UserInput : IUser
    {
        [Required]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public bool IsInactive { get; set; }
    }
}