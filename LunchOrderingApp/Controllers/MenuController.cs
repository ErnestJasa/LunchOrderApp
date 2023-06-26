using LunchOrderingApp.Interfaces;
using LunchOrderingApp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LunchOrderingApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _menuRepository.GetAllMenus();
            return View(menus);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuRepository.AddMenu(menu);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add menu");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _menuRepository.GetMenuById(id);
            return View(menu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _menuRepository.UpdateMenu(id, menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            await _menuRepository.DeleteMenu(id);
            return RedirectToAction("Index");
        }
    }
}
