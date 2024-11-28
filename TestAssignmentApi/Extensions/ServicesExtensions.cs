using TestAssignmentApi.Services.Clients;

namespace TestAssignmentApi.Extensions;

public static class ServicesExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services) =>
        services.AddScoped<IClientService, ClientService>();
}
