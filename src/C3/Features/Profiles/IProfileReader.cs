using System.Threading;
using System.Threading.Tasks;

namespace C3.Features.Profiles
{
    public interface IProfileReader
    {
        Task<ProfileEnvelope> ReadProfile(string username, CancellationToken cancellationToken);
    }
}