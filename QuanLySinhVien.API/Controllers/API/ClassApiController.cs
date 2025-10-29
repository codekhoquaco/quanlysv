using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.ApiControllers
{
    [Route("api/[controller]")] // Base Route: api/ClassApi
    [ApiController]
    public class ClassApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClassApi/GetAllClasses
        // Dùng [HttpGet("GetAllClasses")] thay vì [HttpGet] + [Route("GetAllClasses")]
        [HttpGet("GetAllClasses")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _context.Classes
                .FromSqlRaw("CALL GetAllClasses()")
                .ToListAsync();
            return Ok(classes);
        }
        // GET: api/ClassApi/5
        [HttpGet("{id}")]
        public IActionResult GetClassById(int id)
        {
            var cls = _context.Classes
                .FromSqlRaw($" CALL GetClassById({id})")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();

            if (cls == null)
                return NotFound();
            
            return Ok(cls);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddClass([FromBody] Class cls)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddClass({0}, {1}, {2}, {3})",
                    cls.ClassName,
                    cls.FacultyID,
                    cls.AcademicYear,
                    cls.HomeroomTeacher
                );
                return Ok(new { message = "Lớp mới đã được thêm thành công." });
            }
            catch (Exception ex)
            {
                // Sử dụng InnerException để lấy lỗi chi tiết hơn nếu có
                return StatusCode(500, new { message = "Lỗi khi thêm lớp vào cơ sở dữ liệu.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }        
        // PUT: api/ClassApi/5
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditClass(int id, [FromBody] Class model)
        {
            if (id != model.ClassID)
            {
                return BadRequest(new { message = "ID trong URL và ID trong body không khớp." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL UpdateClass({0}, {1}, {2}, {3}, {4})",
                    model.ClassID,
                    model.ClassName,
                    model.FacultyID,
                    model.AcademicYear,
                    model.HomeroomTeacher
                );

                return Ok(new { message = "Cập nhật lớp thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật lớp.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        // DELETE: api/ClassApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                // Tối ưu hóa: Gọi SP xóa trực tiếp
                var affectedRows = await _context.Database.ExecuteSqlRawAsync("CALL DeleteClass({0})", id);
                
                if (affectedRows == 0)
                {
                    // Nếu không có hàng nào bị ảnh hưởng, có thể ID không tồn tại
                    return NotFound(new { message = "Không tìm thấy lớp để xóa." });
                }

                return Ok(new { message = "Xóa lớp thành công" });
            }
            catch (Exception ex)
            {
                // Đây là nơi bắt lỗi khóa ngoại (Foreign Key) nếu sinh viên đang tham chiếu đến lớp này
                return StatusCode(500, new { message = "Lỗi khi xóa lớp. Có thể lớp này đang được tham chiếu bởi các sinh viên hoặc dữ liệu khác.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}