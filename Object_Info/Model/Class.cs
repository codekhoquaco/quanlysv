using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Models
{
    [Table("classes")]
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClassName { get; set; }

        [Required]
        public int FacultyID { get; set; }

        [MaxLength(50)]
        public string AcademicYear { get; set; }

        [Required]
        public int HomeroomTeacher { get; set; }
    }
}
