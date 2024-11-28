using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Data;

namespace TestAssignmentApi.Extensions;

public static class DatabaseExtensions
{
    public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
}
