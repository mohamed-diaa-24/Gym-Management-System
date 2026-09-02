using GymManagementSystem.Models;
using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;


    public class TrainersController : Controller
    {
        private readonly ITrainerService _service;
        public TrainersController(ITrainerService service) => _service = service;

        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "True";

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var trainers = (await _service.GetAllAsync()).AsQueryable();
            var paged = await PaginatedList<Trainer>.CreateAsync(trainers.OrderBy(t => t.Name).AsQueryable(), pageIndex, 5);
            return View(paged);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer trainer)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            if (!ModelState.IsValid) return View(trainer);
            await _service.CreateAsync(trainer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var trainer = await _service.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainer trainer)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            if (id != trainer.Id) return NotFound();
            if (!ModelState.IsValid) return View(trainer);
            await _service.UpdateAsync(trainer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var trainer = await _service.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var trainer = await _service.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
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