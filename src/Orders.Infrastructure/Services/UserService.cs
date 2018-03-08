using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Exceptions;
using Orders.Infrastructure.Repositories.Extensions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;

        }
        public async Task<JsonWebTokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailOrFailAsync(email, ErrorCode.invalid_credentials, "Invalid credentials");
            if(user.PasswordIsInvalid(password, _passwordHasher))
            {
                throw new ServiceException(ErrorCode.invalid_credentials, "Invalid credentials");
            }

        }

        public async Task RegisterAsync(Guid id, string name, string email, string password, Role role = Role.User)
        {
            var user = await _userRepository.ValidateUserAsync(id, name, email, role);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);
        }
    }
}