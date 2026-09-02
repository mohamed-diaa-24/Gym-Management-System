using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;

[Authorize]
public class ClassesController : Controller
{
    private readonly IGymClassService _classService;
    private readonly ITrainerService _trainerService;

    public ClassesController(IGymClassService classService, ITrainerService trainerService)
    {
        _classService = classService;
        _trainerService = trainerService;
    }

    public async Task<IActionResult> Index(int? trainerId, string search)
    {
        var classes = trainerId.HasValue
            ? await _classService.GetByTrainerIdAsync(trainerId.Value)
            : await _classService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(search))
            classes = classes.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        var vm = new ClassesIndexViewModel
        {
            Classes = classes,
            Trainers = await _trainerService.GetAllAsync(),
            SelectedTrainerId = trainerId,
            SearchTerm = search
        };
        return View(vm);
    }

    public async Task<IActionResult> FilterByTrainer(int? trainerId, string search)
    {
        var classes = trainerId.HasValue
            ? await _classService.GetByTrainerIdAsync(trainerId.Value)
            : await _classService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(search))
            classes = classes.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return PartialView("_ClassesPartial", classes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var gymClass = await _classService.GetWithDetailsAsync(id);
        if (gymClass == null) return NotFound();
        return View(gymClass);
    }
}