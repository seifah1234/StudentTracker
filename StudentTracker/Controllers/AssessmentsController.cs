using Microsoft.AspNetCore.Mvc;
using StudentTracker.BLL.DTOs;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Entities;
namespace StudentTracker.PL.ControllerBases
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentsController : ControllerBase
    {
        private IAssessmentService _assessmentService;

        public AssessmentsController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAssessment([FromBody] AssessmentDto assessment)
        {
            var addedAssessment = await _assessmentService.AddAssessmentAsync(assessment);
            return Ok(addedAssessment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssessments(int? studentId = null, int? subjectId = null, DateTime? date = null)
        {
            var assessments = await _assessmentService.GetAllAssessmentsAsync(studentId, subjectId, date);
            return Ok(assessments);
        }    
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssessmentById(int id)
        {
            var assessment = await _assessmentService.GetAssessmentByIdAsync(id);
            if(assessment==null)
            {
                return NotFound();
            }
            return Ok(assessment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssessment(int id, [FromBody] AssessmentDto assessment)
        {
            var assessmentUpdate=await _assessmentService.GetAssessmentByIdAsync(id);
            if(assessmentUpdate == null)
            {
                return NotFound();
            }
            await _assessmentService.UpdateAssessmentAsync(id, assessment);
            return Ok(assessmentUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            await _assessmentService.DeleteAssessmentAsync(id);
            return Ok(new { message = "Assessment deleted successfully" });
        }
    }
}
