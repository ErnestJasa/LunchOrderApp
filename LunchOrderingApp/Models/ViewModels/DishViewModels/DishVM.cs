using LunchOrderingApp.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace LunchOrderingApp.Models.ViewModels.DishViewModels
{
    public class DishVM
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public string DishImage { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
