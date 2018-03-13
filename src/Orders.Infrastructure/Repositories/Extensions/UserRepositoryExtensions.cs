using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Core.Repositories;
using Orders.Infrastructure.Exceptions;

namespace Orders.Infrastructure.Repositories.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static async Task<IEnumerable<User>> GetAllOrFailAsync(this IUserRepository userRepository)
        {
            var users = await userRepository.GetAllAsync();
            if(users is null)
            {
                throw new ServiceException(ErrorCode.user_not_found, "No users availible.");
            }

            return users;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if(user is null)
            {
                throw new ServiceException(ErrorCode.user_not_found, $"User with given id '{id}' not found.");
            }

            return user;            
        }

        public static async Task<User> GetByEmailOrFailAsync(this IUserRepository userRepository, string email,
        string errorCode, string errorMessage)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if(user is null)
            {
                throw new ServiceException(errorCode, errorMessage);
            }

            return user;
        }

        public static async Task<User> GetByNameOrFailAsync(this IUserRepository userRepository, string name)
        {
            var user = await userRepository.GetByEmailAsync(name);
            if(user is null)
            {
                throw new ServiceException(ErrorCode.user_not_found, $"User with given name '{name}' not found.");
            }

            return user;
        }

        public static async Task<User> ValidateUserAsync(this IUserRepository userRepository, Guid id, string name, string email, Role role)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if (!(user is null))
            {
                throw new ServiceException(ErrorCode.user_already_exists, $"User with given email '{email}' already exist. User email must be unique.");
            }

            user = await userRepository.GetByNameAsync(name);
            if (!(user is null))
            {
                throw new ServiceException(ErrorCode.user_already_exists, $"User with given namne '{name}' already exist. User name must be unique.");
            }

            user = new User(id, name, email, role);
            return user;
        }

        public static async Task RemoveOrFailAsync(this IUserRepository userRepository, Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if (user is null)
            {
                throw new ServiceException(ErrorCode.user_not_found, $"User with given id '{id}' not found. Unable to remove nonexiting user.");
            }
            await userRepository.RemoveAsync(user);
        }
        
    }
}