using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;

namespace StudentTracker.MVC.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApiService _apiService;

        public StudentsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _apiService.GetListAsync<StudentViewModel>("Students");
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _apiService.GetAsync<StudentViewModel>($"Students/{id}");
            if (student == null) return NotFound();

            var totalLevel = await _apiService.GetAsync<int?>($"Students/total-level/{id}");
            var subjectsLevels = await _apiService.GetListAsync<string>($"Students/subjectsLevels/{id}");

            ViewBag.TotalLevel = totalLevel;
            ViewBag.SubjectsLevels = subjectsLevels;
            return View(student);
        }

        public async Task<IActionResult> Create()
        {
            var classRooms = await _apiService.GetListAsync<ClassRoomViewModel>("ClassRooms");
            ViewData["ClassRooms"] = classRooms.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(new StudentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PostAsync("Students", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء إضافة الطالب");
                return View(model);
            }

            TempData["Success"] = "تم إضافة الطالب بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _apiService.GetAsync<StudentViewModel>($"Students/{id}");
            if (student == null) return NotFound();
            var classRooms = await _apiService.GetListAsync<ClassRoomViewModel>("ClassRooms");
            ViewData["ClassRooms"] = classRooms.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // الـ API الحالي بياخد Student.Id من جسم الطلب نفسه، لكن الـ route لسه محتاج {id}
            model.Id = id;
            var response = await _apiService.PutAsync($"Students/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تعديل بيانات الطالب");
                return View(model);
            }

            TempData["Success"] = "تم تعديل بيانات الطالب بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // ملاحظة: الـ API الحالي لا يحتوي على DeleteStudent endpoint،
        // لذلك شاشة الحذف هنا معروضة للاطلاع فقط وتظهر تنبيه بعدم توفر العملية بعد.
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _apiService.GetAsync<StudentViewModel>($"Students/{id}");
            if (student == null) return NotFound();
            return View(student);
        }
    }
}
