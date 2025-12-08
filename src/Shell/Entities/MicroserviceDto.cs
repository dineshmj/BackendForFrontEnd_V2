namespace FW.PAS.BFFWeb.Entities;

public sealed class MicroserviceDto
{
    public string Name { get; init; } = string.Empty;

    public string BaseURL { get; init; } = string.Empty;

    public List<ManagementAreaDto> ManagementAreas { get; init; } = new List<ManagementAreaDto>();
}