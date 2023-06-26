using LunchOrderingApp.Areas.Identity.Data;
using LunchOrderingApp.Interfaces;
using LunchOrderingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LunchOrderingApp.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool AddMenu(Menu Menu)
        {
            _context.Menus.Add(Menu);
            return Save();
        }

        public async Task<bool> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menu);
            return Save();
        }

        public async Task<IEnumerable<Menu>> GetAllMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> GetMenuById(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task<bool> UpdateMenu(int id, Menu menu)
        {
            var dbMenu = await _context.Menus.FindAsync(id);
            dbMenu.MenuName = menu.MenuName;
            _context.Menus.Update(dbMenu);
            return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
