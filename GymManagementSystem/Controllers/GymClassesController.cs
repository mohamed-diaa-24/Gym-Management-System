using GymManagementSystem.Models;
using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;

public class GymClassesController : Controller
    {
        private readonly IGymClassService _service;
        private readonly ITrainerService _trainerService;

        public GymClassesController(IGymClassService service, ITrainerService trainerService)
        {
            _service = service;
            _trainerService = trainerService;
        }

        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "True";

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var classes = (await _service.GetAllAsync()).AsQueryable();
            var paged = await PaginatedList<GymClass>.CreateAsync(classes.OrderBy(c => c.Name).AsQueryable(), pageIndex, 5);
            return View(paged);
        }

        public async Task<IActionResult> Create()
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            ViewBag.Trainers = await _trainerService.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GymClass gymClass)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            if (!ModelState.IsValid)
            {
                ViewBag.Trainers = await _trainerService.GetAllAsync();
                return View(gymClass);
            }
            await _service.CreateAsync(gymClass);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var gymClass = await _service.GetByIdAsync(id);
            if (gymClass == null) return NotFound();
            ViewBag.Trainers = await _trainerService.GetAllAsync();
            return View(gymClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GymClass gymClass)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            if (id != gymClass.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.Trainers = await _trainerService.GetAllAsync();
                return View(gymClass);
            }
            await _service.UpdateAsync(gymClass);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var gymClass = await _service.GetWithDetailsAsync(id);
            if (gymClass == null) return NotFound();
            return View(gymClass);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var gymClass = await _service.GetByIdAsync(id);
            if (gymClass == null) return NotFound();
            return View(gymClass);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }