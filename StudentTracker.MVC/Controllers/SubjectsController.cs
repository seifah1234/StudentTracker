using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;

namespace StudentTracker.MVC.Controllers
{
    [Authorize]
    public class SubjectsController : Controller
    {
        private readonly ApiService _apiService;

        public SubjectsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _apiService.GetListAsync<SubjectViewModel>("Subjects");
            return View(subjects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subject = await _apiService.GetAsync<SubjectViewModel>($"Subjects/{id}");
            if (subject == null) return NotFound();
            subject.Id = id;
            return View(subject);
        }

        public IActionResult Create() => View(new SubjectViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PostAsync("Subjects", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء إضافة المادة");
                return View(model);
            }

            TempData["Success"] = "تم إضافة المادة بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _apiService.GetAsync<SubjectViewModel>($"Subjects/{id}");
            if (subject == null) return NotFound();
            subject.Id = id;
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubjectViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PutAsync($"Subjects/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تعديل المادة");
                return View(model);
            }

            TempData["Success"] = "تم تعديل المادة بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _apiService.GetAsync<SubjectViewModel>($"Subjects/{id}");
            if (subject == null) return NotFound();
            subject.Id = id;
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync($"Subjects/{id}");
            TempData["Success"] = "تم حذف المادة بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}
