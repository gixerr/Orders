using Orders.Infrastructure.Commands.Interfaces;

namespace Orders.Infrastructure.Commands.Users
{
    public class Login : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}