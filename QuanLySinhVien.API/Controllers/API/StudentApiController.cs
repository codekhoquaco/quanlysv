using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using System.Linq; // Cần thiết cho FirstOrDefault()
using System;

namespace QuanLySinhVien.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. GET: Lấy tất cả sinh viên (Dùng SP GetAllStudents) ---
        // ✅ GET: api/StudentApi/GetAllStudents
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students
                .FromSqlRaw("CALL GetAllStudents()")
                .ToListAsync();
            return Ok(students);
        }

        // --- 2. GET: Lấy sinh viên theo ID (Dùng SP GetStudentById) ---
        // ✅ GET: api/StudentApi/5
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            // Sử dụng SingleOrDefault() cho trường hợp chỉ trả về 1 dòng.
            // .AsNoTracking() và .AsEnumerable() là cần thiết khi mapping kết quả SP
            var student = _context.Students
                .FromSqlRaw($"CALL GetStudentById({id})")
                .AsNoTracking()
                .AsEnumerable()
                .SingleOrDefault();

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // --- 3. POST: Thêm sinh viên (Dùng SP AddStudent) ---
        // ✅ POST: api/StudentApi/Create
        [HttpPost("Create")]
        public async Task<IActionResult> AddStudent([FromBody] Student model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Gọi SP AddStudent
                // Tên các tham số trong SP phải khớp với thứ tự truyền vào
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddStudent({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Address,
                    model.PhoneNumber,
                    model.Email,
                    model.NationalID,
                    model.EnrollmentDate, 
                    model.ClassID         
                );
                return Ok(new { message = "✅ Thêm sinh viên thành công" });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết hơn trong môi trường thực tế
                return StatusCode(500, new { message = "Lỗi khi thêm sinh viên.", detail = ex.Message });
            }
        }

        // --- 4. PUT: Cập nhật sinh viên (Dùng SP UpdateStudent) ---
        // ✅ PUT: api/StudentApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student model)
        {
            if (id != model.StudentID)
                return BadRequest("Mã sinh viên không khớp");

            try
            {
                int affectedRows = await _context.Database.ExecuteSqlRawAsync(
                    "CALL UpdateStudent({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})",
                    model.StudentID,
                    model.FirstName,
                    model.LastName,
                    model.Gender,
                    model.DateOfBirth,
                    model.Address,
                    model.PhoneNumber,
                    model.Email,
                    model.NationalID,
                    model.EnrollmentDate,
                    model.ClassID
                );

                if (affectedRows > 0)
                    return Ok(new { message = "Cập nhật sinh viên thành công" });

                return StatusCode(500, new { message = "Không có dòng nào bị ảnh hưởng." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật sinh viên.", detail = ex.Message });
            }
        }


        // --- 5. DELETE: Xóa sinh viên (Dùng SP DeleteStudent) ---
        // ✅ DELETE: api/StudentApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                // Gọi SP DeleteStudent
                await _context.Database.ExecuteSqlRawAsync("CALL DeleteStudent({0})", id);
                return Ok(new { message = "🗑️ Xóa sinh viên thành công" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu ID không tồn tại hoặc có ràng buộc khóa ngoại
                return StatusCode(500, new { message = "Lỗi khi xóa sinh viên.", detail = ex.Message });
            }
        }
    }
}