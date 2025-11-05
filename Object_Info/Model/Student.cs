using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Student
    {
        [Key]
        [Display(Name = "Id Sinh Viên")]
        public int StudentID { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Họ Sinh Viên")]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Tên Sinh Viên")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Giới Tính")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Sinh")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(255)]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [MaxLength(20)]
        [Display(Name = "Số Điện Thoại")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        [Display(Name = "Mã Số Sinh Viên")]
        public string NationalID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Nhập Học")]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "ID Lớp Học")]
        public int ClassID { get; set; } // chỉ là cột, không có FK

        public string ClassName { get; set; } // Thuộc tính bổ sung để hiển thị tên lớp
    }
}

