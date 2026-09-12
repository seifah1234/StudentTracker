using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;

namespace StudentTracker.MVC.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly ApiService _apiService;

        public AttendanceController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? studentId, DateTime? date)
        {
            var query = new List<string>();
            if (studentId.HasValue) query.Add($"studentId={studentId}");
            if (date.HasValue) query.Add($"date={date:yyyy-MM-dd}");

            var endpoint = "Attendance" + (query.Count > 0 ? "?" + string.Join("&", query) : string.Empty);
            var records = await _apiService.GetListAsync<AttendanceViewModel>(endpoint);

            ViewBag.StudentId = studentId;
            ViewBag.Date = date;
            return View(records);
        }

        public async Task<IActionResult> Details(int id)
        {
            var attendance = await _apiService.GetAsync<AttendanceViewModel>($"Attendance/{id}");
            if (attendance == null) return NotFound();
            attendance.Id = id;
            return View(attendance);
        }

        public IActionResult Create() => View(new AttendanceViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttendanceViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PostAsync("Attendance", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تسجيل الحضور");
                return View(model);
            }

            TempData["Success"] = "تم تسجيل الحضور بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var attendance = await _apiService.GetAsync<AttendanceViewModel>($"Attendance/{id}");
            if (attendance == null) return NotFound();
            attendance.Id = id;
            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AttendanceViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PutAsync($"Attendance/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تعديل سجل الحضور");
                return View(model);
            }

            TempData["Success"] = "تم تعديل سجل الحضور بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var attendance = await _apiService.GetAsync<AttendanceViewModel>($"Attendance/{id}");
            if (attendance == null) return NotFound();
            attendance.Id = id;
            return View(attendance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync($"Attendance/{id}");
            TempData["Success"] = "تم حذف سجل الحضور بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}
