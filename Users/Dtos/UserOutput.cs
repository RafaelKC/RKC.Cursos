using System;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users.Dtos
{
    public class UserOutput: IUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        public bool IsInactive { get; set; }

        public UserOutput(User userInput)
        {
            Id = userInput.Id;
            FirstName = userInput.FirstName;
            LastName = userInput.LastName;
            UserName = userInput.UserName;
            Role = userInput.Role;
            IsInactive = userInput.IsInactive;
        }
    }
}