namespace Orders.Infrastructure.Dtos
{
    public class JsonWebTokenDto
    {
        public string AccessToken { get; set; }
        public long Expires { get; set; }
    }
}