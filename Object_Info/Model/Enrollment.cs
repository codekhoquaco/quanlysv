using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Enrollment
    {
        [Key]
        [Display(Name = "Id Đăng Ký")]
        public int EnrollmentID { get; set; }
        [Display(Name = "Id Sinh Viên")]
        public int StudentID { get; set; }
        [Display(Name = "Id Môn Học")]
        public int SubjectID { get; set; }
        [Display(Name = "Id Học Kỳ")]
        public int SemesterID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Đăng Ký")]
        public DateTime EnrollDate { get; set; }
    }
}
