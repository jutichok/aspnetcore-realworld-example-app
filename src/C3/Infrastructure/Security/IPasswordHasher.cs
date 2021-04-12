using System.Threading.Tasks;

namespace C3.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        Task<byte[]> Hash(string password, byte[] salt);
    }
}