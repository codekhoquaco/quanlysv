using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Models
{
    [Table("classes")]
    public class Class
    {
        [Key]
        [Display(Name = "Id Lớp Học")]
        public int ClassID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Tên Lớp Học")]
        public string ClassName { get; set; }

        [Required]
        [Display(Name = "Id Khoa")]
        public int FacultyID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Năm Học")]
        public string AcademicYear { get; set; }

        [Required]
        [Display(Name = "Id Giáo Viên Chủ Nhiệm")]
        public int HomeroomTeacher { get; set; }
    }
}
