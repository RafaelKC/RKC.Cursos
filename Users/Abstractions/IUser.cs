using System.ComponentModel.DataAnnotations;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Abstractions
{
    public interface IUser
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        public string UserName { get; set; }
        public UserRole  Role { get; set; }
        public bool IsInactive { get; set; }
    }
}