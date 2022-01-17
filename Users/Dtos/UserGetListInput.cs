using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Dtos
{
    public class UserGetListInput
    {
        public string FilterByUserName { get; set; } 
        public UserRole? FilterByUserRole { get; set; } 
        public bool GetInactivesToo { get; set; }
    }
}