using Microsoft.AspNetCore.Http;

namespace LunchOrderingApp.Models.ViewModels.DishViewModels
{
    public class CreateAndEditDishVM
    {
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public IFormFile DishImage { get; set; }
        public int MenuId { get; set; }
    }
}
