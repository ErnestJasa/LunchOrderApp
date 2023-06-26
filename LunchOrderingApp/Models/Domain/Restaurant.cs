#nullable enable
using System.ComponentModel.DataAnnotations.Schema;

namespace LunchOrderingApp.Models.Domain
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantCode { get; set; }
        [ForeignKey(nameof(Domain.Menu.MenuId))]
        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
