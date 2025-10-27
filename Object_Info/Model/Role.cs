using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class Role
    {
        [Key]
        [Display(Name = "Id Vai Trò")]
        public int RoleID { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Tên Vai Trò")]
        public string RoleName { get; set; }

        [MaxLength(200)]
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }
    }
}
