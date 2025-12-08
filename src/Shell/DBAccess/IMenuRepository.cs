using FW.PAS.BFFWeb.Entities;

namespace FW.PAS.BFFWeb.DBAccess;

public interface IMenuRepository
{
    Task<List<MenuDetail>> GetAuthorizedMenuAsync(IEnumerable<string> userRoles);
}