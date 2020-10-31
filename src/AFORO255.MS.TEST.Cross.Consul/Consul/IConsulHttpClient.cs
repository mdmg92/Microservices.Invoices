using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Cross.Consul.Consul
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}

