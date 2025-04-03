using System.Reflection;
using System.Text.RegularExpressions;

namespace CapitalSix.AspNetCore.Extensions;

internal static class AssemblyExtensions
{
    internal static string? GetAssemblyAttribute<T>(this Assembly assembly, Func<T, string> get) where T : Attribute
    {
        // Get attributes of this type.
        object[] attributes =
            assembly.GetCustomAttributes(typeof(T), true);

        // If we didn't get anything, return null.
        if (attributes == null || attributes.Length == 0)
            return null;

        // Convert the first attribute value into
        // the desired type and return it.
        return get((T)attributes[0]);
    }

    internal static AssemblyInfo GetAssemblyVersionInfo(this Assembly assembly)
    {
        var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
        var fileVersion = $"{version.FileMajorPart}.{version.FileMinorPart}.{version.FileBuildPart}";
        var informationalVersion = assembly.GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(a => a.InformationalVersion);
        string commitHash = string.Empty, subVersion = string.Empty;
        if (!string.IsNullOrEmpty(informationalVersion))
        {
            var match = Regex.Match(informationalVersion, @"(?<version>\d+[.]\d+[.]\d+)([-]*(?<subversion>[^+]*))([+](?<commit>.*))");
            if (match.Success)
            {
                subVersion = match.Groups["subversion"].Value;
                commitHash = match.Groups["commit"].Value;
            }
        }

        return new AssemblyInfo()
        {
            InformationalVersion = informationalVersion ?? string.Empty,
            Title = assembly.GetAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title) ?? string.Empty,
            Description = assembly.GetAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description) ?? string.Empty,
            Version = fileVersion,
            ProductVersion = version.ProductVersion ?? string.Empty,
            FileVersion = version.FileVersion ?? string.Empty,
            SubVersion = subVersion,
            CommitHash = commitHash,
            ShortCommitHash = commitHash.Length >= 6 ? commitHash.Substring(0, 6) : string.Empty,
        };
    }
}
