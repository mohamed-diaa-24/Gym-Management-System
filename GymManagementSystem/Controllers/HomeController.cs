using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}