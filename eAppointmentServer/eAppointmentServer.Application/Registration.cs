using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace eAppointmentServer.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }

        
    }
}
