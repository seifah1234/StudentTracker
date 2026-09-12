using System.ComponentModel.DataAnnotations;

namespace StudentTracker.MVC.Models
{
    public class ClassRoomViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الفصل مطلوب")]
        [Display(Name = "اسم الفصل")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "المدرّس المسؤول مطلوب")]
        [Display(Name = "المدرّس المسؤول")]
        public int TeacherId { get; set; }

        [Display(Name = "الفصل الدراسي")]
        public Semester Semester { get; set; }

        [Display(Name = "العدد المتوقع للطلاب")]
        [Range(0, int.MaxValue, ErrorMessage = "قيمة غير صحيحة")]
        public int ExpectedStudentCount { get; set; }

        [Required(ErrorMessage = "العام الدراسي مطلوب")]
        [Display(Name = "العام الدراسي")]
        public string AcademicYear { get; set; } = string.Empty;
    }
}
