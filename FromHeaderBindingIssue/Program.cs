using Asp.Versioning;
using FromHeaderBindingIssue;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ApiVersionReader =
            new HeaderApiVersionReader("x-api-version");
    })
    .AddApiExplorer(apiExplorerOptions =>
    {
        apiExplorerOptions.GroupNameFormat = "'v'VVV";
        apiExplorerOptions.SubstituteApiVersionInUrl = true;
    });
builder.Services
    .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()
    .AddSwaggerGen();

var application = builder.Build();

if (application.Environment.IsDevelopment())
{
    application
        .UseSwagger()
        .UseSwaggerUI(swaggerOptions =>
        {
            IEnumerable<string> descriptions = application
                .DescribeApiVersions()
                .Select(description => description.GroupName);
            foreach (var description in descriptions)
            {
                string url = $"/swagger/{description}/swagger.json";
                string name = description.ToUpperInvariant();
                swaggerOptions.SwaggerEndpoint(url, name);
            }
        });
}

application.MapApis();

application.Run();

