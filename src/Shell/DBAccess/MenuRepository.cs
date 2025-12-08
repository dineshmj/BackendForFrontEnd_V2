using Microsoft.EntityFrameworkCore;

using FW.PAS.BFFWeb.Entities;

namespace FW.PAS.BFFWeb.DBAccess;

public sealed class MenuRepository
    : IMenuRepository
{
    private readonly MenuDbContext _context;

    public MenuRepository(MenuDbContext context)
    {
        _context = context;
    }

    private const string MENU_SQL_QUERY = @"
        SELECT
            ms.Name AS Microservice,
            ms.BaseURL,
            ma.Name AS ManagementAreaName,
            mi.TaskName,
            mi.UrlRelativePath,
            mi.IconName,
            mra.RoleShortName
        FROM
            Microservices ms
        INNER JOIN
            ManagementArea ma ON ms.ID = ma.MicroserviceID
        INNER JOIN
            MenuItems mi ON ma.ID = mi.ManagementAreaID
        INNER JOIN
            MenuItemsAndRoles mra ON mi.ID = mra.MenuItemID
        WHERE
            mra.RoleShortName IN ({0}) -- Placeholder for comma-separated parameter names
        ORDER BY
            ms.ID,
            ma.ID,
            mi.ID,
            mra.RoleShortName;";

    public async Task<List<MenuDetail>> GetAuthorizedMenuAsync(IEnumerable<string> userRoles)
    {
        if (userRoles == null || !userRoles.Any())
        {
            return new List<MenuDetail>();
        }

        var parameters = new List<string>();
        var parameterPlaceholders = new List<string>();

        for (int i = 0; i < userRoles.Count(); i++)
        {
            var paramName = $"@p{i}";
            parameterPlaceholders.Add(paramName);
            parameters.Add(userRoles.ElementAt(i));
        }

        string inClause = string.Join(", ", parameterPlaceholders);
        string finalSql = string.Format(MENU_SQL_QUERY, inClause);

        // 3. Execute the query using the MenuDetail entity as the result type
        return await _context.MenuDetails
            .FromSqlRaw(finalSql, parameters.ToArray())
            .ToListAsync();
    }
}