using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Abstractions
{
    public interface IUser
    {
        public string UserName { get; set; }
        public UserRole  Role { get; set; }
    }
}