using System;
using Microsoft.AspNet.Mvc;
using SampleWebApp.Models;

namespace SampleWebApp.Controllers
{
    public class NearsoftController : Controller
    {
        public IActionResult Index()
        {
            var nearsoft = new Nearsoft();
            return View(nearsoft);
        }
    }
}