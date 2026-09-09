using Microsoft.AspNetCore.Mvc;
using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SubjectsController : ControllerBase
    {
        private ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] SubjectDto subject)
        {
            await _subjectService.CreateSubjectAsync(subject);
            return Ok(subject);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if(subject==null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDto subject)
        {
            await _subjectService.UpdateSubjectAsync(id, subject);
            return Ok(subject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            await _subjectService.DeleteSubjectAsync(id);
            return Ok(new { message = "Subject deleted successfully" });
        }   
    }
}
