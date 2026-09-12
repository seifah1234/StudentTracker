using System.ComponentModel.DataAnnotations;

namespace StudentTracker.MVC.Models
{
    // ملاحظة: AssessmentDto في الـ API الحالي لا يحتوي على خاصية Id.
    public class AssessmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم التقييم مطلوب")]
        [Display(Name = "اسم التقييم")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "المادة مطلوبة")]
        [Display(Name = "المادة")]
        public int SubjectId { get; set; }

        [Display(Name = "اسم المادة")]
        public string? SubjectName { get; set; }

        [Required(ErrorMessage = "الطالب مطلوب")]
        [Display(Name = "الطالب")]
        public int StudentId { get; set; }

        [Display(Name = "اسم الطالب")]
        public string? StudentName { get; set; }

        [Display(Name = "الدرجة النهائية")]
        [Range(0, double.MaxValue, ErrorMessage = "قيمة غير صحيحة")]
        public decimal MaximumMarks { get; set; }

        [Display(Name = "الدرجة المحصلة")]
        [Range(0, double.MaxValue, ErrorMessage = "قيمة غير صحيحة")]
        public decimal ObtainedMarks { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "التاريخ مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
