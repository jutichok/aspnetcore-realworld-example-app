namespace C3.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(string username);
    }
}