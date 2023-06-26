using LunchOrderingApp.Interfaces;
using LunchOrderingApp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LunchOrderingApp.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task<IActionResult> Index()
        {
            var restaurants = await _restaurantRepository.GetAllRestaurants();
            return View(restaurants);
        }
        public async Task<IActionResult> Add()
        {
            ViewBag.Menus = await _restaurantRepository.GetMenus();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Restaurant restaurant)
        {
            ViewBag.Menus = await _restaurantRepository.GetMenus();
            if (ModelState.IsValid)
            {
                _restaurantRepository.AddRestaurant(restaurant);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add Restaurant");
            }
            return View(restaurant);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Menus = await _restaurantRepository.GetMenus();
            var restaurant = await _restaurantRepository.GetRestaurantById(id);
            return View(restaurant);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Restaurant restaurant)
        {
            ViewBag.Menus = await _restaurantRepository.GetMenus();
            if (ModelState.IsValid)
            {
                await _restaurantRepository.UpdateRestaurant(id, restaurant);
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        public async Task<IActionResult> Detail(int id, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var dishes = await _restaurantRepository.GetDishes(id, searchString);
            return View(dishes);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _restaurantRepository.DeleteRestaurant(id);
            return RedirectToAction("Index");
        }
    }
}
