using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Semester
    {
        [Key]
        [Display(Name = "Id Học Kỳ")]
        public int SemesterID { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Tên Học Kỳ")]
        public string SemesterName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Năm học")]
        public string AcademicYear { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }
    }
}
