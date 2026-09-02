using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;

public class TrainersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}