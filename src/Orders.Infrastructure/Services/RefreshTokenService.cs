using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Orders.Core.Domain;
using Orders.Core.Exceptions;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Exceptions;
using Orders.Infrastructure.Services.Interfaces;

namespace Orders.Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private static readonly ISet<RefreshTokenDto> RefreshTokens = new HashSet<RefreshTokenDto>();

        public RefreshTokenService(IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public RefreshTokenDto Get(string token)
            => RefreshTokens.SingleOrDefault(x => x.Token == token);

        public void Add(RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto is null)
            {
                throw new ServiceException(ErrorCode.token_empty, "Adding refresh token faild. Token can not be null.");
            }

            RefreshTokens.Add(refreshTokenDto);
        }

        public RefreshTokenDto Create(User user, Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ServiceException(ErrorCode.invalid_guid, "Creating refresh token failed. Guid can not be empty.");
            }

            if (user is null)
            {
                throw new ServiceException(ErrorCode.invalid_user, "Creating refresh token failed. Guid can not be empty.");
            }
            var token = _passwordHasher.HashPassword(user, guid.ToString());

            return new RefreshTokenDto
            {
                UserId = user.Id,
                UserRole = user.Role,
                Token = token
            };
        }

        public TokenDto RefreshAccessToken(string token)
        {
            var refreshToken = Get(token);
            if (refreshToken is null)
            {
                throw new ServiceException(ErrorCode.token_not_found, "Refreshing token failed. Token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new ServiceException(ErrorCode.token_revoked, "Refreshing token failed. Token was revoked.");
            }

            var jwt = _jwtService.CreateToken(refreshToken.UserId, refreshToken.UserRole);

            return new TokenDto
            {
                AccessToken = jwt.AccessToken,
                RefreshToken = refreshToken.Token,
                Expires = jwt.Expires,
                Role = refreshToken.UserRole
            };
        }

        public void RevokeRefreshToken(string token)
        {
            var refreshToken = Get(token);
            if (refreshToken is null)
            {
                throw new ServiceException(ErrorCode.token_empty, "Revoking token failed. Token was not found.");
            }

            if (refreshToken.Revoked)
            {
                throw new ServiceException(ErrorCode.token_revoked, "Revoking refresh token failed. Token was already revoked.");
            }

            refreshToken.Revoked = true;
        }
    }
}