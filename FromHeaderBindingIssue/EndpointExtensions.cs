using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Routing;

internal static class EndpointExtensions
{

    public static WebApplication MapApis(this WebApplication application)
    {
        application
            .MapService1Api()
            .MapService2Api();
        return application;
    }

    private static WebApplication MapService1Api(this WebApplication application)
    {
        var apiName = "Individuals";
        var routeName = apiName.Replace(' ', '-').ToLower();
        var group = string.Concat("/customers/{customerId:int}/", routeName);
        var api = application
            .NewApiVersionSet(apiName)
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .AdvertisesApiVersion(new ApiVersion(1, 0))
            .Build();
        var routeGroupBuilder = application
            .MapGroup(group)
            .WithApiVersionSet(api)
            .WithTags(apiName);
        routeGroupBuilder
            .MapGet("/", (
                [FromHeader(Name = "x-api-version")] string apiVersion,
                [FromRoute] int customerId,
                [FromServices] ILoggerFactory loggerFactory,
                [FromServices] LinkGenerator linkGenerator
            ) =>
            {
                return new List<string> { "Individual1", "Individual2" };
            })
            .WithName("GetIndividuals")
            .WithDescription("Get a list of individuals for a customer.")
            .WithSummary("Get a list of individuals for a customer.")
            .WithOpenApi()
            .Produces<List<string>>(200)
            .Produces<List<string>>(404);
        return application;
    }

    private static WebApplication MapService2Api(this WebApplication application)
    {
        var apiName = "Businesses";
        var routeName = apiName.ToLower();
        var group = string.Concat("/customers/{customerId:int}/", routeName);
        var api = application
            .NewApiVersionSet(apiName)
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .AdvertisesApiVersion(new ApiVersion(1, 0))
            .Build();
        var routeGroupBuilder = application
            .MapGroup(group)
            .WithApiVersionSet(api)
            .WithTags(apiName);
        routeGroupBuilder
            .MapGet("/", (
                [FromHeader(Name = "x-api-version")] string apiVersion,
                [FromRoute] int customerId,
                [FromServices] ILoggerFactory loggerFactory,
                [FromServices] LinkGenerator linkGenerator
            ) =>
            {
                return new List<string> { "Business 1", "Business 2" };
            })
            .WithName("GetBusinesses")
            .WithDescription("Get a list of businesses for a customer.")
            .WithSummary("Get a list of businesses for a customer.")
            .WithOpenApi()
            .Produces<List<string>>(200)
            .Produces<List<string>>(404);
        return application;
    }

}
