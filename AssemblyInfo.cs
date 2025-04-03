namespace CapitalSix.AspNetCore.Extensions;

/// <summary>
/// 
/// </summary>
internal class AssemblyInfo : IAssemblyInfo
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string ProductVersion { get; set; } = string.Empty;
    public string FileVersion { get; set; } = string.Empty;
    public string InformationalVersion { get; set; } = string.Empty;
    public string SubVersion { get; set; } = string.Empty;
    public string CommitHash { get; set; } = string.Empty;
    public string ShortCommitHash { get; set; } = string.Empty;
}
