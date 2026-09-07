using Microsoft.AspNetCore.Mvc;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.Controllers
{
    [ApiController]
    [Route("api/[SubjectsController]")]
    public class SubjectsController : ControllerBase
    {
        private static List<Subject> subjects = new List<Subject>();
        [HttpPost("add")]
        public IActionResult AddSubject([FromBody] Subject subject)
        {
            subjects.Add(subject);
            return Ok(subject);
        }
        [HttpGet("all")]
        public IActionResult GetAllSubjects()
        {
            return Ok(subjects);
        }       
        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            var subject = subjects.FirstOrDefault(s=>s.Id==id);
            if(subject==null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateSubject([FromBody] Subject subject)
        {
            var subjectId=subject.Id;
            var subjectUpdate=subjects.FirstOrDefault(s=>s.Id==subjectId);
            if(subjectUpdate==null)
            {
                return NotFound();
            }
            subjectUpdate.Name=subject.Name;
            subjectUpdate.Assessments=subject.Assessments;  
            subjectUpdate.MaximumMarks=subject.MaximumMarks;
            subjectUpdate.Description=subject.Description;
            return Ok(subjectUpdate);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSubject(int id)
        {
            var subject=subjects.FirstOrDefault(s=>s.Id==id);
            if(subject==null)
            {
                return NotFound();
            }
            subjects.Remove(subject);
            return Ok(new { message = "Subject deleted successfully" });
        }   
    }
}
