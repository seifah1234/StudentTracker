using System.ComponentModel.DataAnnotations;

namespace StudentTracker.MVC.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الطالب مطلوب")]
        [Display(Name = "الاسم")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "رقم هاتف ولي الأمر")]
        public string? ParentPhoneNumber { get; set; }

        [Required(ErrorMessage = "الفصل مطلوب")]
        [Display(Name = "الفصل")]
        public int ClassRoomId { get; set; }

        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Today.AddYears(-10);

        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [Display(Name = "الرقم القومي")]
        public string NationalId { get; set; } = string.Empty;

        [Display(Name = "تاريخ الالتحاق")]
        [DataType(DataType.Date)]
        public DateTime EnrollementDate { get; set; } = DateTime.Today;

        [Display(Name = "رابط الصورة الشخصية")]
        public string? PersonalPhotoUrl { get; set; }
        public string? ClassRoomName { get; set; }
    }
}
