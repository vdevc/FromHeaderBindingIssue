using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FromHeaderBindingIssue;

internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "My Api Service " + description.ApiVersion.ToString(),
            Version = description.ApiVersion.ToString(),
            Description = "A simple example ASP.NET Core Web API",
            Contact = new OpenApiContact()
            {
                Name = "Api Service Contact",
                Email = "email@apiservice.com",
                Url = new Uri("https://apiservice.com")
            },
            TermsOfService = new Uri("https://apiservice.com/terms"),
            License = new OpenApiLicense()
            {
                Name = "Use under LICX",
                Url = new Uri("https://apiservice.com/license")
            }
        };
        return info;
    }

}

