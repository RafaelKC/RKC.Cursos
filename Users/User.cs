using System;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}