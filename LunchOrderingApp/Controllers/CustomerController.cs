﻿using Microsoft.AspNetCore.Mvc;

namespace LunchOrderingApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
