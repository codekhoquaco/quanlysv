using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Subject
    {
        [Key]
        [Display(Name = "Id Môn Học")]
        public int SubjectID { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Tên Môn Học")]
        public string SubjectName { get; set; }

        [Range(1, 10)]
        [Display(Name = "Số Tín Chỉ")]
        public int Credits { get; set; }
        [Display(Name = "Id Khoa")]
        public int FacultyID { get; set; }
    }
}
