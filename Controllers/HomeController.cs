using Microsoft.AspNet.Mvc;
using SampleWebApp.Models;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var nearsoftian = new Nearsoftian(){Name="Gus", Phone="87687687"};
            return new ObjectResult(nearsoftian);
        }
    }
}