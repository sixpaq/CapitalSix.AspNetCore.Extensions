namespace CapitalSix.AspNetCore.Extensions;

/// <summary>
/// Contains information which is extracted from your assambly. It
/// contains version information, Git information and title and
/// description.
/// </summary>
public interface IAssemblyInfo
{
    /// <summary>
    /// The title of the assembly
    /// </summary>
    string Title { get; }
    /// <summary>
    /// The description of the assembly
    /// </summary>
    string Description { get; }
    /// <summary>
    /// The version of the assembly in format: 1.0.0
    /// </summary>
    string Version { get; }
    /// <summary>
    /// The full product version
    /// </summary>
    string ProductVersion { get; }
    /// <summary>
    /// The full file version
    /// </summary>
    string FileVersion { get; }
    /// <summary>
    /// The version part of the informational version
    /// </summary>
    string InformationalVersion { get; }
    /// <summary>
    /// This is the sub version if present. This can
    /// be "alpha" or "beta".
    /// </summary>
    string SubVersion { get; }
    /// <summary>
    /// This is the full Id of the commit that is
    /// used for the build
    /// </summary>
    string CommitHash { get; }
    /// <summary>
    /// This is the short version of the commit hash.
    /// This is essentially the first 6 digits of the
    /// commit hash.
    /// </summary>
    string ShortCommitHash { get; }
}
