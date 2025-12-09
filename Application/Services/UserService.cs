using Application.Contracts.Services;
using Application.DTOs.User;
using Domain.Entities;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _context;
        private readonly IAuditService _audit;

        public UserService(AppDbContext context, IAuditService audit)
        {
            _context =  context;
            _audit = audit; 
        }

        public async Task<UserDto> RegisterUserAsync (RegisterUserRequest request)
        {
            // Check email unique
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return null;

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                //PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)

                PasswordHash = request.Password// To Be Hashed 
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _audit.LogAsync("RegisterUser", "User", user.Id, $"User {user.Email} registered");

            return new UserDto { Id = user.Id  , Email =user.Email , Name= user .Name};
        }
    }
}
