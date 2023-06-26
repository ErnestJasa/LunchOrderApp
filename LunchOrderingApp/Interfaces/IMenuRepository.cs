using LunchOrderingApp.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LunchOrderingApp.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<Menu> GetMenuById(int id);
        bool AddMenu(Menu Menu);
        Task<bool> UpdateMenu(int id, Menu menu);
        Task<bool> DeleteMenu(int id);
    }
}
