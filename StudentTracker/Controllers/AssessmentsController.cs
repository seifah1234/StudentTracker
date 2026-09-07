using Microsoft.AspNetCore.Mvc;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.ControllerBases
{
    [ApiController]
    [Route("api/[AssessmentsController]")]
    public class AssessmentsController : ControllerBase
    {
        private static List<Assessment> assessments = new List<Assessment>();
        [HttpPost("add")]
        public IActionResult AddAssessment([FromBody] Assessment assessment)
        {
            assessments.Add(assessment);
            return Ok(assessment);
        }
        [HttpGet("all")]
        public IActionResult GetAllAssessments()
        {
            return Ok(assessments);
        }       
        [HttpGet("{id}")]
        public IActionResult GetAssessmentById(int id)
        {
            var assessment = assessments.FirstOrDefault(s=>s.Id==id);
            if(assessment==null)
            {
                return NotFound();
            }
            return Ok(assessment);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAssessment([FromBody] Assessment assessment)
        {
            var assessmentId=assessment.Id;
            var assessmentUpdate=assessments.FirstOrDefault(s=>s.Id==assessmentId);
            if(assessmentUpdate==null)
            {
                return NotFound();
            }
            assessmentUpdate.Name=assessment.Name;
            assessmentUpdate.MaximumMarks=assessment.MaximumMarks;  
            assessmentUpdate.Notes=assessment.Notes;
            assessmentUpdate.ObtainedMarks=assessment.ObtainedMarks;
            return Ok(assessmentUpdate);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAssessment(int id)
        {
            var assessment=assessments.FirstOrDefault(s=>s.Id==id);
            if(assessment==null)
            {
                return NotFound();
            }
            assessments.Remove(assessment);
            return Ok(new { message = "Assessment deleted successfully" });
        }
        [HttpGet("student/{studentId}")]
        public IActionResult GetAssessmentsByStudentId(int studentId)
        {
            var studentAssessments = assessments.Where(s => s.StudentId == studentId).ToList();
            return Ok(studentAssessments);
        }
        [HttpGet("subject/{subjectId}")]
        public IActionResult GetAssessmentsBySubjectId(int subjectId)
        {
            var subjectAssessments = assessments.Where(s => s.SubjectId == subjectId).ToList();
            return Ok(subjectAssessments);
        }
        [HttpGet("date/{date}")]
        public IActionResult GetAssessmentsByDate(DateTime date)
        {
            var dateAssessments = assessments.Where(s => s.Date == date).ToList();
            return Ok(dateAssessments);
        }
    }
}
