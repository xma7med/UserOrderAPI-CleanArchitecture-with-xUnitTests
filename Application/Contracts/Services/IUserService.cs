using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public  interface IUserService
    {

        public Task<UserDto> RegisterUserAsync(RegisterUserRequest request);

    }
}
