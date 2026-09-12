using System.ComponentModel.DataAnnotations;

namespace StudentTracker.MVC.Models
{
    // ملاحظة: SubjectDto في الـ API الحالي لا يحتوي على خاصية Id،
    // لذلك يتم تمرير الـ Id بشكل منفصل عبر الـ route عند التعديل/الحذف/العرض.
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "كود المادة مطلوب")]
        [Display(Name = "الكود")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم المادة مطلوب")]
        [Display(Name = "اسم المادة")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "الفصل مطلوب")]
        [Display(Name = "الفصل")]
        public int ClassRoomId { get; set; }

        [Display(Name = "الوصف")]
        public string? Description { get; set; }

        [Display(Name = "الدرجة النهائية")]
        [Range(0, double.MaxValue, ErrorMessage = "قيمة غير صحيحة")]
        public decimal MaximumMarks { get; set; }
    }
}
