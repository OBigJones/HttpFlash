using Data;
using Data.Repository;
using Domain.User;
using HTTPHeadersFlashMeeting.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace HTTPHeadersFlashMeeting.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<Authentication>();
            services.AddScoped<LoginAuth>();

            return services;
        }
    }
}
