using CleanArchMediatR.Template.Domain.Interfaces.Services;
using CleanArchMediatR.Template.Infra.Logging;
using CleanArchMediatR.Template.Infra.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CleanArchMediatR.Template.Infra.Extensions
{
    public static class InfraServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services) { 

            Log.Logger = SerilogConfigurator.Configure();

            services.AddAutoMapper(typeof(EntityMappingProfile));
            services.AddSingleton<ILoggerService, SerilogLoggerService>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });

            return services;
        }
    }
}
