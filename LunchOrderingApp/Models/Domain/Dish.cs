#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace LunchOrderingApp.Models.Domain
{
    public class Dish
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set;}
        public byte[] DishImage { get; set; }
        [ForeignKey(nameof(Domain.Menu.MenuId))]
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
