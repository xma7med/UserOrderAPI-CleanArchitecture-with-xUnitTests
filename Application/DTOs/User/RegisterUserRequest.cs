using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class RegisterUserRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [Required, EmailAddress]
        public string Email { get; set; } = default!;
        [Required, MinLength(6)]
        public string Password { get; set; } = default!;
    }


    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
