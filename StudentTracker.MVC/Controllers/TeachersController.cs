using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;

namespace StudentTracker.MVC.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private readonly ApiService _apiService;

        public TeachersController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _apiService.GetListAsync<TeacherViewModel>("Teachers");
            return View(teachers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _apiService.GetAsync<TeacherViewModel>($"Teachers/{id}");
            if (teacher == null) return NotFound();
            return View(teacher);
        }

        public IActionResult Create() => View(new TeacherViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PostAsync("Teachers", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء إضافة المدرّس");
                return View(model);
            }

            TempData["Success"] = "تم إضافة المدرّس بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _apiService.GetAsync<TeacherViewModel>($"Teachers/{id}");
            if (teacher == null) return NotFound();
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Id = id;
            var response = await _apiService.PutAsync($"Teachers/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تعديل بيانات المدرّس");
                return View(model);
            }

            TempData["Success"] = "تم تعديل بيانات المدرّس بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _apiService.GetAsync<TeacherViewModel>($"Teachers/{id}");
            if (teacher == null) return NotFound();
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync($"Teachers/{id}");
            TempData["Success"] = "تم حذف المدرّس بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ClassRooms(int id)
        {
            var classRooms = await _apiService.GetListAsync<ClassRoomViewModel>($"Teachers/classroom/{id}");
            var teacher = await _apiService.GetAsync<TeacherViewModel>($"Teachers/{id}");
            ViewBag.TeacherName = teacher?.Name ?? $"#{id}";
            ViewBag.TeacherId = id;
            return View(classRooms);
        }

        public async Task<IActionResult> Students(int id)
        {
            var students = await _apiService.GetListAsync<StudentViewModel>($"Teachers/students/{id}");
            var teacher = await _apiService.GetAsync<TeacherViewModel>($"Teachers/{id}");
            ViewBag.TeacherName = teacher?.Name ?? $"#{id}";
            ViewBag.TeacherId = id;
            return View(students);
        }
    }
}
