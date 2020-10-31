using System.Threading.Tasks;

namespace AFORO255.MS.TEST.Cross.Consul.Fabio
{
    public interface IFabioHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}