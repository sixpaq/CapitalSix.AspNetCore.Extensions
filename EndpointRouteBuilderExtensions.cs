using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace CapitalSix.AspNetCore.Extensions;

/// <summary>
/// Extension class for Endpoint router in startup
/// </summary>
public static class EndpointRouterBuilderExtensions
{
    /// <summary>
    /// Map an endpoint that reveals the current assembly version
    /// </summary>
    /// <param name="endpointRouteBuilder"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapVersion(this IEndpointRouteBuilder endpointRouteBuilder, string pattern = "/version")
    {
        var logger = endpointRouteBuilder.ServiceProvider.GetService<ILogger<AssemblyInfo>>();
        var assemblyInfo = endpointRouteBuilder.ServiceProvider.GetRequiredService<IAssemblyInfo>();
        var applicationLifetime = endpointRouteBuilder.ServiceProvider.GetService<IHostApplicationLifetime>();
        var server = endpointRouteBuilder.ServiceProvider.GetService<IServer>();

        applicationLifetime?.ApplicationStarted.Register(() =>
        {
            var serverAddressesFeature = server?.Features.Get<IServerAddressesFeature>();
            var addresses = serverAddressesFeature?.Addresses;

            if (addresses?.Count > 0)
            {
                foreach (var url in addresses)
                {
                    logger?.LogInformation(@"Mapped version endpoint to: {Pattern}", new Uri(new Uri(url), pattern).ToString());
                }
            }
            else
            {
                logger?.LogInformation(@"Mapped version endpoint to: {Pattern}", pattern);
            }
        });


        var responseString = $@"{{""version"":""{assemblyInfo.Version}"",""commit"":""{assemblyInfo.ShortCommitHash}""}}";

        endpointRouteBuilder.MapGet(pattern, async httpContext =>
        {
            httpContext.Response.Headers.Append(
                HeaderNames.ContentType,
                MediaTypeNames.Application.Json);
            await httpContext.Response.WriteAsync(responseString);
            logger?.LogDebug("Version information request handled");
        });

        return endpointRouteBuilder;
    }
}
