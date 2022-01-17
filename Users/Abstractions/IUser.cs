using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Abstractions
{
    public interface IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public UserRole  Role { get; set; }
        public bool IsInactive { get; set; }
    }
}