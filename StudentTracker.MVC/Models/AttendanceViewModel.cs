using System.ComponentModel.DataAnnotations;

namespace StudentTracker.MVC.Models
{
    // ملاحظة: AttendanceDto في الـ API الحالي لا يحتوي على خاصية Id.
    // الإضافة تعتمد على خاصية AttendanceStatus (Enum) بينما التعديل يعتمد على خاصية Status (نص).
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الطالب مطلوب")]
        [Display(Name = "الطالب")]
        public int StudentId { get; set; }

        [Display(Name = "اسم الطالب")]
        public string? StudentName { get; set; }

        [Required(ErrorMessage = "التاريخ مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Display(Name = "الحالة")]
        public AttendanceStatus AttendanceStatus { get; set; }

        // يُستخدم عند العرض والتعديل (النص القادم من الـ API)
        [Display(Name = "الحالة")]
        public string? Status { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
    }
}
