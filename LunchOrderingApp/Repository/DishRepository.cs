using LunchOrderingApp.Areas.Identity.Data;
using LunchOrderingApp.Interfaces;
using LunchOrderingApp.Models.Domain;
using LunchOrderingApp.Models.ViewModels.DishViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderingApp.Repository
{
    public class DishRepository : IDishRepository
    {
        private readonly AppDbContext _context;

        public DishRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDish(CreateAndEditDishVM dish)
        {
            using var memoryStream = new MemoryStream();
            await dish.DishImage.CopyToAsync(memoryStream);
            var imageByteData = memoryStream.ToArray();
            var newDish = new Dish
            {
                DishName = dish.DishName,
                DishDescription = dish.DishDescription,
                DishImage = imageByteData,
                MenuId = dish.MenuId,
            };
            _context.Dishes.Add(newDish);
            return Save();
        }

        public async Task<bool> DeleteDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dish);
            return Save();
        }

        public async Task<IEnumerable<DishVM>> GetDishes(string searchString)
        {
            IQueryable<Dish> dishes = from d in _context.Dishes
                                      select d;
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(x => x.DishDescription.Contains(searchString) || x.DishName.Contains(searchString));
            }
            await dishes.ToListAsync();
            List<DishVM> dishVMs = new List<DishVM>();
            foreach (var dish in dishes)
            {
                DishVM dishVM = new DishVM
                {
                    DishId = dish.DishId,
                    DishName = dish.DishName,
                    DishDescription = dish.DishDescription,
                    MenuId = dish.MenuId,
                    Menu = dish.Menu,
                };
                if (dish.DishImage is not null)
                {
                    string imageBase64Data = Convert.ToBase64String(dish.DishImage);
                    dishVM.DishImage = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }
                dishVMs.Add(dishVM);
            }
            return dishVMs;
        }

        public async Task<DishVM> GetDishById(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            DishVM dishVM = new DishVM
            {
                DishId = dish.DishId,
                DishName = dish.DishName,
                DishDescription = dish.DishDescription,
                MenuId = dish.MenuId,
                Menu = dish.Menu,
            };
            if (dish.DishImage is not null)
            {
                string imageBase64Data = Convert.ToBase64String(dish.DishImage);
                dishVM.DishImage = string.Format("data:image/png;base64,{0}", imageBase64Data);
            }
            return dishVM;
        }

        public async Task<bool> UpdateDish(int id, CreateAndEditDishVM dish)
        {
            var dbDish = await GetDishUncoverted(id);
            if (dbDish is not null)
            {
                dbDish.DishName = dish.DishName;
                dbDish.DishDescription = dish.DishDescription;
                dbDish.MenuId = dish.MenuId;
                if (dish.DishImage is not null)
                {
                    using var memoryStream = new MemoryStream();
                    await dish.DishImage.CopyToAsync(memoryStream);
                    var imageByteData = memoryStream.ToArray();
                    dbDish.DishImage = imageByteData;
                }
            }
            _context.Dishes.Update(dbDish);
            return Save();
        }
        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }
        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        private async Task<Dish> GetDishUncoverted(int id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task<IEnumerable<DishVM>> GetDishesByMenuId(int menuId)
        {

            //IQueryable<Dish> dishes = from d in _context.Dishes
            //                          select d;
            
            //dishes = dishes.Where(x => x.MenuId == menuId);
            //await dishes.ToListAsync();
            var dishes = await _context.Dishes.Where(x => x.MenuId == menuId).ToListAsync();
            
            List<DishVM> dishVMs = new List<DishVM>();
            foreach (var dish in dishes)
            {
                DishVM dishVM = new DishVM
                {
                    DishId = dish.DishId,
                    DishName = dish.DishName,
                    DishDescription = dish.DishDescription,
                    MenuId = dish.MenuId,
                    Menu = dish.Menu,
                };
                if (dish.DishImage is not null)
                {
                    string imageBase64Data = Convert.ToBase64String(dish.DishImage);
                    dishVM.DishImage = string.Format("data:image/png;base64,{0}", imageBase64Data);
                }
                dishVMs.Add(dishVM);
            }
            return dishVMs;
        }
    }
}
