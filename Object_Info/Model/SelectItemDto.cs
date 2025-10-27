using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization; // Thêm thư viện này nếu cần tùy chỉnh tên thuộc tính

namespace Object_Info.Model
{
    // DTO chung cho tất cả các danh sách thả xuống
    public class SelectItemDto
    {
        // Thuộc tính dùng làm giá trị (value) của thẻ <option> (ví dụ: FacultyID, TeacherID)
        // Dùng object để linh hoạt cho cả int (ID) và string (Teacher Homeroom, User Name, v.v.)
        public object? Id { get; set; }

        // Thuộc tính dùng làm văn bản hiển thị (text) của thẻ <option> (ví dụ: Tên Khoa, Tên Giáo viên)
        public string? Name { get; set; }
    }
}