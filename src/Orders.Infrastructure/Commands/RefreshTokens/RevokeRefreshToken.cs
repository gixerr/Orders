using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.RefreshTokens
{
    public class RevokeRefreshToken : ICommand
    {
        public string RefreshToken { get; set; }
    }
}