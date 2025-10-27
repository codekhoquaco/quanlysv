using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Faculty
    {
        [Key]
        [Display(Name = "Id Khoa")]
        public int FacultyID { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Tên Khoa")]
        public string FacultyName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Vị trí văn phòng")]
        public string OfficeLocation { get; set; }

        [MaxLength(20)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
