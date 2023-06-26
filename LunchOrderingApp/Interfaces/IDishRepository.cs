using LunchOrderingApp.Models.Domain;
using LunchOrderingApp.Models.ViewModels.DishViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LunchOrderingApp.Interfaces
{
    public interface IDishRepository
    {
        Task<IEnumerable<DishVM>> GetDishes(string searchString);
        Task<DishVM> GetDishById(int id);
        Task<bool> AddDish(CreateAndEditDishVM dish);
        Task<bool> UpdateDish(int id, CreateAndEditDishVM dish);
        Task<bool> DeleteDish(int id);
        Task<IEnumerable<DishVM>> GetDishesByMenuId(int menuId);
        Task<IEnumerable<Menu>> GetMenus();
    }
}
