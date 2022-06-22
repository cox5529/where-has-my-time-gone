using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WhereHasMyTimeGone.API.Gateway.Filters;

public class ExceptionOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add(
            "400",
            new OpenApiResponse
            {
                Description = "Bad Request",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "application/json",
                        new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(
                                typeof(ValidationProblemDetails),
                                context.SchemaRepository)
                        }
                    }
                }
            });
        operation.Responses.Add(
            "404",
            new OpenApiResponse
            {
                Description = "Not Found",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "application/json",
                        new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(
                                typeof(ProblemDetails),
                                context.SchemaRepository)
                        }
                    }
                }
            });
        operation.Responses.Add(
            "500",
            new OpenApiResponse
            {
                Description = "Internal Server Error",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    {
                        "application/json",
                        new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(
                                typeof(ProblemDetails),
                                context.SchemaRepository)
                        }
                    }
                }
            });
    }
}
