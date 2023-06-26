using LunchOrderingApp.Models.Domain;
using LunchOrderingApp.Models.ViewModels.DishViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LunchOrderingApp.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant> GetRestaurantById(int id);
        bool AddRestaurant(Restaurant restaurant);
        Task<bool> UpdateRestaurant(int id, Restaurant restaurant);
        Task<bool> DeleteRestaurant(int id);
        Task<IEnumerable<DishVM>> GetDishes(int menuId, string searchString);
        Task<IEnumerable<Menu>> GetMenus();
    }
}
