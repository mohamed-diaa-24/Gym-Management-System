using GymManagementSystem.Models;
using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;


    public class MembersController : Controller
    {
        private readonly IMemberService _service;
        public MembersController(IMemberService service) => _service = service;

        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "True";

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var members = (await _service.GetAllAsync()).AsQueryable();
            var paged = await PaginatedList<Member>.CreateAsync(members.OrderBy(m => m.Name).AsQueryable(), pageIndex, 5);
            return View(paged);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            if (!ModelState.IsValid) return View(member);
            await _service.CreateAsync(member);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var member = await _service.GetByIdAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            if (id != member.Id) return NotFound();
            if (!ModelState.IsValid) return View(member);
            await _service.UpdateAsync(member);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var member = await _service.GetByIdAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
            var member = await _service.GetByIdAsync(id);
            if (member == null) return NotFound();
            return View(member);
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