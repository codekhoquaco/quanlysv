using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Models
{
    [Table("classes")]      
    public class Class
    {
        [Key]
        [Display(Name = "Id Lớp")]
        public int ClassID { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Tên Lớp")]
        public string ClassName { get; set; }
        [Display(Name = "Id Khoa")]
        public int FacultyID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Năm học")]
        public string AcademicYear { get; set; }

        [MaxLength(100)]
        [Display(Name = "Giáo viên chủ nhiệm")]
        public int  HomeroomTeacher { get; set; }
    }
    public class ClassListItem
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
    }
}
