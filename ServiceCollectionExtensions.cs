using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CapitalSix.AspNetCore.Extensions;

/// <summary>
/// This class contains an extension method for registering
/// version info to a service collection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This method extracts the version info from the entry
    /// assembly and adds it to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddVersionInfo(this IServiceCollection services)
    {
        services.AddSingleton<IAssemblyInfo>(_ => Assembly.GetEntryAssembly()!.GetAssemblyVersionInfo());
        return services;
    }
}
