using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.RefreshTokens
{
    public class RefreshAccessToken : ICommand
    {
        public string RefreshToken { get; set; }
    }
}