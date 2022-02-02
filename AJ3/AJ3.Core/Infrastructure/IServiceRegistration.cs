using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AJ3.Core.Infrastructure
{
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration);
    }
}
