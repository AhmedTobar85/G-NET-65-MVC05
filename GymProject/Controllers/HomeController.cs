using Microsoft.AspNetCore.Mvc;

namespace GymProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
