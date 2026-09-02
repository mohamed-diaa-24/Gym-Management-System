using GymManagementSystem.Services.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers;


public class EnrollmentsController : Controller
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly IMemberService _memberService;
    private readonly IGymClassService _classService;

    public EnrollmentsController(IEnrollmentService enrollmentService, IMemberService memberService, IGymClassService classService)
    {
        _enrollmentService = enrollmentService;
        _memberService = memberService;
        _classService = classService;
    }

    private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "True";

    public async Task<IActionResult> Create()
    {
        if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");
        var vm = new EnrollMemberViewModel
        {
            Members = await _memberService.GetAllAsync(),
            GymClasses = await _classService.GetAllAsync()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EnrollMemberViewModel model)
    {
        if (!IsAdmin()) return RedirectToAction("AccessDenied", "Account");

        if (!ModelState.IsValid)
        {
            model.Members = await _memberService.GetAllAsync();
            model.GymClasses = await _classService.GetAllAsync();
            return View(model);
        }

        var (success, message) = await _enrollmentService.EnrollAsync(model.MemberId, model.GymClassId, model.EnrollmentDate);
        TempData[success ? "Success" : "Error"] = message;

        if (!success)
        {
            model.Members = await _memberService.GetAllAsync();
            model.GymClasses = await _classService.GetAllAsync();
            return View(model);
        }

        return RedirectToAction(nameof(Create));
    }
}