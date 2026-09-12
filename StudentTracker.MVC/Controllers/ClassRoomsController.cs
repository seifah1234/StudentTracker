using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;

namespace StudentTracker.MVC.Controllers
{
    [Authorize]
    public class ClassRoomsController : Controller
    {
        private readonly ApiService _apiService;

        public ClassRoomsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var classRooms = await _apiService.GetListAsync<ClassRoomViewModel>("ClassRooms");
            return View(classRooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var classRoom = await _apiService.GetAsync<ClassRoomViewModel>($"ClassRooms/{id}");
            if (classRoom == null) return NotFound();
            return View(classRoom);
        }

        public async Task<IActionResult> Create()
        {
            var teachers = await _apiService.GetListAsync<TeacherViewModel>("Teachers");
            ViewData["Teachers"] = teachers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(new ClassRoomViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassRoomViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PostAsync("ClassRooms", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء إضافة الفصل");
                return View(model);
            }

            TempData["Success"] = "تم إضافة الفصل بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var classRoom = await _apiService.GetAsync<ClassRoomViewModel>($"ClassRooms/{id}");
            if (classRoom == null) return NotFound();
            var teachers = await _apiService.GetListAsync<TeacherViewModel>("Teachers");
            ViewData["Teachers"] = teachers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(classRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassRoomViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            model.Id = id;
            var response = await _apiService.PutAsync($"ClassRooms/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تعديل الفصل");
                return View(model);
            }

            TempData["Success"] = "تم تعديل الفصل بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var classRoom = await _apiService.GetAsync<ClassRoomViewModel>($"ClassRooms/{id}");
            if (classRoom == null) return NotFound();
            var teachers = await _apiService.GetListAsync<TeacherViewModel>("Teachers");
            ViewData["Teachers"] = teachers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(classRoom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync($"ClassRooms/{id}");
            TempData["Success"] = "تم حذف الفصل بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Students(int id)
        {
            var students = await _apiService.GetListAsync<StudentViewModel>($"ClassRooms/students/{id}");
            var classRoom = await _apiService.GetAsync<ClassRoomViewModel>($"ClassRooms/{id}");
            ViewBag.ClassRoomName = classRoom?.Name ?? $"#{id}";
            ViewBag.ClassRoomId = id;
            return View(students);
        }
    }
}
