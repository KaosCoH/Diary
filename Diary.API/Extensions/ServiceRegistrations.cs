using Diary.Data.Services;
using Diary.Data.Services.Interfaces;

namespace Diary.API.Extensions
{
    public static class ServiceRegistrations
    {
        /// <summary>
        /// Extension method used to register all custom API services.
        /// </summary>
        /// <param name="serviceCollection"> Service collection on which the services will be registered. </param>
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsersService, UsersService>();
        }
    }
}
