using ApiBackendChallenge.Utility;
using Datalibrary.DataAccess;
using Datalibrary.ProductRepository;

namespace ApiBackendChallenge.StartUp
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSingleton<UtilityClass>();

            return services;
            
        }
    }
}
