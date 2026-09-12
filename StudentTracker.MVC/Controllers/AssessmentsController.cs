using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTracker.MVC.Models;
using StudentTracker.MVC.Services;

namespace StudentTracker.MVC.Controllers
{
    [Authorize]
    public class AssessmentsController : Controller
    {
        private readonly ApiService _apiService;

        public AssessmentsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? studentId, int? subjectId, DateTime? date)
        {
            var query = new List<string>();
            if (studentId.HasValue) query.Add($"studentId={studentId}");
            if (subjectId.HasValue) query.Add($"subjectId={subjectId}");
            if (date.HasValue) query.Add($"date={date:yyyy-MM-dd}");

            var endpoint = "Assessments" + (query.Count > 0 ? "?" + string.Join("&", query) : string.Empty);
            var assessments = await _apiService.GetListAsync<AssessmentViewModel>(endpoint);

            ViewBag.StudentId = studentId;
            ViewBag.SubjectId = subjectId;
            ViewBag.Date = date;
            return View(assessments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var assessment = await _apiService.GetAsync<AssessmentViewModel>($"Assessments/{id}");
            if (assessment == null) return NotFound();
            assessment.Id = id;
            return View(assessment);
        }

        public IActionResult Create() => View(new AssessmentViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssessmentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PostAsync("Assessments", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء إضافة التقييم");
                return View(model);
            }

            TempData["Success"] = "تم إضافة التقييم بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var assessment = await _apiService.GetAsync<AssessmentViewModel>($"Assessments/{id}");
            if (assessment == null) return NotFound();
            assessment.Id = id;
            return View(assessment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssessmentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _apiService.PutAsync($"Assessments/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "حدث خطأ أثناء تعديل التقييم");
                return View(model);
            }

            TempData["Success"] = "تم تعديل التقييم بنجاح";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var assessment = await _apiService.GetAsync<AssessmentViewModel>($"Assessments/{id}");
            if (assessment == null) return NotFound();
            assessment.Id = id;
            return View(assessment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync($"Assessments/{id}");
            TempData["Success"] = "تم حذف التقييم بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}
