using System;
using System.Threading.Tasks;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Services.Interfaces
{
    public interface IRefreshTokenService : IService
    {
        RefreshTokenDto Get(string token);
        void Add(RefreshTokenDto refreshTokenDto);
        RefreshTokenDto Create(User user, Guid guid);
        TokenDto RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
    }
}