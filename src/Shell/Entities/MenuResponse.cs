namespace FW.PAS.BFFWeb.Entities;

public sealed class MenuResponse
{
    public List<MicroserviceDto> Microservices { get; init; } = new List<MicroserviceDto>();
}