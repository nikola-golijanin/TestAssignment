using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestAssignmentApi.Extensions;

public static class SwaggerExtensions
{
    public static void RegisterSwaggerUI(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<CustomHeaderParameter>();
            c.EnableAnnotations();
        });

    public class CustomHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-API-Key",
                In = ParameterLocation.Header,
                Description = "Custom header for authentication",
                Required = false,
            });
        }
    }
}
