using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required, MaxLength(255)]
        [Display(Name = "Mật khẩu")]
        public string PasswordHash { get; set; }
        [Display(Name = "Id Vai Trò")]
        public int RoleID { get; set; }
        [Display(Name = "Id Sinh Viên")]
        public int? StudentID { get; set; }
        [Display(Name = "Id Giảng Viên")]
        public int? TeacherID { get; set; }
        [Display(Name = "Hoạt động")]
        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Vai Trò")]
        public string Role { get; set; }
    }
}
