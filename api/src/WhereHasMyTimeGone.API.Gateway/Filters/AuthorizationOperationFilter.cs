using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WhereHasMyTimeGone.API.Gateway.Filters;

public class AuthorizationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
        var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
        var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);
        if (!isAuthorized || allowAnonymous)
        {
            return;
        }

        operation.Responses.Add(
            "401",
            new OpenApiResponse
            {
                Description = "Unauthorized",
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
            "403",
            new OpenApiResponse
            {
                Description = "Forbidden",
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

        operation.Parameters ??= new List<OpenApiParameter>();
        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            }
        };
    }
}
