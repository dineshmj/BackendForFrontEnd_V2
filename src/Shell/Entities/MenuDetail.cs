namespace FW.PAS.BFFWeb.Entities;

public sealed class MenuDetail
{
    public string Microservice { get; set; } = string.Empty;

    public string BaseURL { get; set; } = string.Empty;

    public string ManagementAreaName { get; set; } = string.Empty;

    public string TaskName { get; set; } = string.Empty;

    public string UrlRelativePath { get; set; } = string.Empty;

    public string IconName { get; set; } = string.Empty;

    public string RoleShortName { get; set; } = string.Empty;
}