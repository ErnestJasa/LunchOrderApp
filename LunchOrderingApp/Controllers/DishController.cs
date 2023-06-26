using LunchOrderingApp.Interfaces;
using LunchOrderingApp.Models.ViewModels.DishViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LunchOrderingApp.Controllers
{
    public class DishController : Controller
    {
        private readonly IDishRepository _dishRepository;

        public DishController(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var dishes = await _dishRepository.GetDishes(searchString);
            return View(dishes);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add()
        {
            ViewBag.Menus = await _dishRepository.GetMenus();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateAndEditDishVM dishVM)
        {
            ViewBag.Menus = await _dishRepository.GetMenus();
            if (ModelState.IsValid)
            {
                await _dishRepository.AddDish(dishVM);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(dishVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Menus = await _dishRepository.GetMenus();
            var dish = await _dishRepository.GetDishById(id);
            if (dish is null)
            {
                return View("Error");
            }
            return View(dish);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAndEditDishVM dishVM)
        {
            ViewBag.Menus = await _dishRepository.GetMenus();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit dish");
                return View("Edit", dishVM);
            }
            if (await _dishRepository.UpdateDish(id, dishVM))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(await _dishRepository.GetDishById(id));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _dishRepository.DeleteDish(id);
            return RedirectToAction("Index");
        }
    }
}
