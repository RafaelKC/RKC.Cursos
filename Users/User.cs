using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RKC.Cursos.Users.Abstractions;
using RKC.Cursos.Users.Dtos;
using RKC.Cursos.Users.Enums;

namespace RKC.Cursos.Users
{
    [Table("Users")]
    public class User : IUser
    {
        [Required]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Column("UserName")] 
        public string _userName { get; set; }

        [NotMapped]
        public string UserName
        {
            get => !string.IsNullOrEmpty(_userName) ? _userName : $"{FirstName} {LastName}";
            set => _userName = value;
        }
        public UserRole Role { get; set; }
        public bool IsInactive { get; set; }

        public void Update(IUser userInput)
        {
            FirstName = userInput.FirstName;
            LastName = userInput.LastName;
            UserName = userInput.UserName;
            Role = userInput.Role;
            IsInactive = userInput.IsInactive;
        }
        
        public User(UserInput userInput)
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