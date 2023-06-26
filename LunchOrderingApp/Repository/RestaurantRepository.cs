using LunchOrderingApp.Areas.Identity.Data;
using LunchOrderingApp.Interfaces;
using LunchOrderingApp.Models.Domain;
using LunchOrderingApp.Models.ViewModels.DishViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderingApp.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _context;

        public RestaurantRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            return Save();
        }

        public async Task<bool> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            return Save();
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantById(int id)
        {
            return await _context.Restaurants.Include(x => x.Menu).FirstOrDefaultAsync(x => x.RestaurantId == id);
        }

        public async Task<bool> UpdateRestaurant(int id, Restaurant restaurant)
        {
            var dbRestaurant = await _context.Restaurants.FindAsync(id);
            dbRestaurant.RestaurantName = restaurant.RestaurantName;
            dbRestaurant.RestaurantAddress = restaurant.RestaurantAddress;
            dbRestaurant.RestaurantCode = restaurant.RestaurantCode;
            dbRestaurant.MenuId = restaurant.MenuId;
            _context.Restaurants.Update(dbRestaurant);
            return Save();

        }
        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<IEnumerable<DishVM>> GetDishes(int menuId, string searchString)
        {

            IQueryable<Dish> dishes = from d in _context.Dishes
                                      select d;

            dishes = dishes.Where(x => x.MenuId == menuId);

            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(x => x.DishName.Contains(searchString) || x.DishDescription.Contains(searchString));
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
    }
}
