using System.Threading.Tasks;
using Consul;

namespace AFORO255.MS.TEST.Cross.Consul.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<AgentService> GetAsync(string name);
    }
}