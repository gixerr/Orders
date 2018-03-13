using Orders.Core.Domain;

namespace Orders.Infrastructure.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public Role Role { get; set; }
        public long Expires { get; set; }
    }
}