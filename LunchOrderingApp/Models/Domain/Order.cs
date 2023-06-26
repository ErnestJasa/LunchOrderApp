using LunchOrderingApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace LunchOrderingApp.Models.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        [ForeignKey(nameof(Domain.Dish.DishId))]
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public string DeliveryAddress { get; set; }
        public int OrderAmount { get; set; }
        [ForeignKey(nameof(ApplicationUser.Id))]
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public bool OrderConfirmed { get; set; } = false;
    }
}
