using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FW.PAS.BFFWeb.DBAccess;

namespace FW.PAS.BFFWeb.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class MenuController
    : ControllerBase
{
    private readonly IMenuRepository _menuRepository;
    private readonly ILogger<MenuController> _logger;

    public MenuController(IMenuRepository menuRepository, ILogger<MenuController> logger)
    {
        _menuRepository = menuRepository;
        _logger = logger;
    }

    [HttpGet("authorized")]
    [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<MenuResponse>> GetAuthorizedMenu()
    {
        var userRoles = GetUserRoles();

        if (!userRoles.Any())
        {
            return Ok(new MenuResponse());
        }

        var flatMenu = await _menuRepository.GetAuthorizedMenuAsync(userRoles);

        if (flatMenu == null || !flatMenu.Any())
        {
            _logger.LogInformation("No menu items found for roles: {Roles}", string.Join(", ", userRoles));
            return Ok(new MenuResponse());
        }

        var microserviceGroups = flatMenu
            .GroupBy(md => new { md.Microservice, md.BaseURL })
            .Select(msGroup => new MicroserviceDto
            {
                Name = msGroup.Key.Microservice,
                BaseURL = msGroup.Key.BaseURL,

                ManagementAreas = msGroup
                    .GroupBy(maGroup => maGroup.ManagementAreaName)
                    .Select(maGroup => new ManagementAreaDto
                    {
                        Name = maGroup.Key,

                        MenuItems = maGroup
                            .DistinctBy(mi => mi.TaskName)
                            .Select(mi => new MenuItemDto
                            {

                                TaskName = mi.TaskName,
                                UrlRelativePath = mi.UrlRelativePath,
                                IconName = mi.IconName
                            })
                            .ToList()
                    })
                    .Where(ma => ma.MenuItems.Any())
                    .ToList()
            })
            .Where(ms => ms.ManagementAreas.Any())
            .ToList();

        return Ok(new MenuResponse
        {
            Microservices = microserviceGroups
        });
    }

    private IEnumerable<string> GetUserRoles()
    {
        // Extract the roles from the signed in user
        var roles
            = User
                .Claims
                .Where(c => c.Type == "role")
                .Select(c => c.Value)
                .ToArray();

        return roles;
    }
}