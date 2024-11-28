using TestAssignmentApi.Services.Clients;
using TestAssignmentApi.Services.Users;

namespace TestAssignmentApi.Extensions;

public static class ServicesExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services) =>
        services.AddScoped<IClientService, ClientService>()
                .AddScoped<IUserService, UserService>();
}
