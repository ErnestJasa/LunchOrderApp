using System.Collections.Generic;

namespace LunchOrderingApp.Models.Domain
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public ICollection<Dish> Dishes { get; set; }
    }
}
