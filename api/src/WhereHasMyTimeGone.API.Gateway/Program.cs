using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Gateway.Filters;
using WhereHasMyTimeGone.API.Gateway.Middleware;
using WhereHasMyTimeGone.API.Gateway.Services;
using WhereHasMyTimeGone.API.Gateway.Settings;
using WhereHasMyTimeGone.API.Infrastructure;
using WhereHasMyTimeGone.API.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddKeyPerFile("/run/secrets", true);
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets(Assembly.GetAssembly(typeof(Program)));
}


// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
       .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            o =>
            {
                o.AllowAnyMethod();
                o.AllowAnyOrigin();
                o.AllowAnyHeader();
            });
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = "WhereHasMyTimeGone.API",
                Description = "Help."
            });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        options.OperationFilter<AuthorizationOperationFilter>();
        options.OperationFilter<ExceptionOperationFilter>();
        options.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });
    });

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.Configure<SlackSettings>(builder.Configuration.GetSection("Slack"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(o => { builder.Configuration.GetSection("JWT").Bind(o); });

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Create database
using (var scope = app.Services.CreateScope())
{
    await ApplicationDbContextSeed.Run(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<SlackRequestValidatorMiddleware>();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WhereHasMyTimeGone.API v1"));

app.Run();
