using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

namespace quanlysv.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FacultyApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Faculty
        [HttpGet]
        [Route("GetAllFaculties")]
        public async Task<IActionResult> GetAllFaculties()
        {
            var faculties = await _context.Faculties
                .FromSqlRaw("CALL GetAllFaculties()")
                .ToListAsync();
            return Ok(faculties);
        }

        // GET: /Faculty/5
        [HttpGet("id/{id}")]
        public IActionResult GetFacultyById(int id)
        {
            var faculty = _context.Faculties
                .FromSqlRaw($" CALL GetFacultyById({id})")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();
            if (faculty == null)
                return NotFound();
            return Ok(faculty);
        }
        // POST: /Faculty
        [HttpPost("Add")]

        public async Task<IActionResult> AddFaculty([FromBody] Faculty faculty)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddFaculty({0}, {1}, {2}, {3})",
                    faculty.FacultyName,
                    faculty.OfficeLocation,
                    faculty.Phone,
                    faculty.Email
                );
                return Ok(new { message = "Khoa mới đã được thêm thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi thêm khoa.", details = ex.Message });
            }
        }

        [HttpPost("Edit")]

        public async Task<IActionResult> UpdateFaculty([FromBody] Faculty faculty)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL UpdateFaculty({0}, {1}, {2}, {3}, {4})",
                    faculty.FacultyID,
                    faculty.FacultyName,
                    faculty.OfficeLocation,
                    faculty.Phone,
                    faculty.Email
                );
                return Ok(new { message = "Cập nhật thông tin khoa thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật khoa.", details = ex.Message });
            }
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteFaculty(int id)
        {
            try
            {
                var affectedRows = await _context.Database.ExecuteSqlRawAsync(
            $"CALL DeleteFaculty({id})");

                if (affectedRows > 0)
                {
                    return Ok(new { message = "Khoa đã được xóa thành công." });
                }
                else
                {
                    // Trả về 404 nếu không có hàng nào bị ảnh hưởng (ID không tồn tại)
                    return NotFound(new { message = "Khoa không tồn tại hoặc đã bị xóa." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa khoa.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
