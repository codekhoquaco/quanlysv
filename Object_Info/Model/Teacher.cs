using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Required, MaxLength(50)]
        [Display(Name = "Họ")]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Tên")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "giớ tính")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(20)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Id Khoa")]
        public int FacultyID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Chức vụ")]
        public string Position { get; set; }
        [NotMapped]
        public string TeacherName { get; set; }
    }
}
