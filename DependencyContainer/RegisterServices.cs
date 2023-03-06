using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Transactions;

namespace DependencyContainer
{
    public static class RegisterServices
    {
        public static void RegisterProjectService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton(Log.Logger);
            //services.AddRequestCorrelation();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.RegisterServices(configuration);

            //services.AddTransient<ICommunicationHelper, CommunicationHelper>();
            //services.AddTransient<ICsoPALCommunication, CsoPALCommunication>();
            //services.AddTransient<ICsoPALCommunicationHelper, CsoPALCommunicationHelper>();
            //services.AddTransient<IExternalCommunication, ExternalCommunication>();
            //services.AddTransient<IExternalApiErrorLogsRepository, ExternalApiErrorLogsRepository>();
            //services.AddTransient<IExternalApiErrorLogsMaster, ExternalApiErrorLogsMaster>();
        }

        public static class DependencyContainer
        {
            public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddScoped<DBContext>(options => new DBContext(configuration.GetConnectionString("PALConnectionString")));
                services.AddScoped<ITransactions, Transactions>();
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            }
        }
    }
}
