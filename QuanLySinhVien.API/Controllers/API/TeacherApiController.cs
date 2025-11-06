using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeacherApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Teacher/GetAllTeachers
        [HttpGet("GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _context.Teachers
                .FromSqlRaw("CALL GetAllTeachers()")
                .ToListAsync();
            return Ok(teachers);
        }
        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _context.Teachers
                .FromSqlRaw($" CALL GetTeacherById({id})")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();
            if (teacher == null)
                return NotFound();
            return Ok(teacher);
        }
        // POST: api/Teacher/Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
                return BadRequest(new {errors = ModelState});
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddTeacher({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                    teacher.FirstName,
                    teacher.LastName,
                    teacher.Gender,
                    teacher.DateOfBirth,
                    teacher.Phone,
                    teacher.Email,
                    teacher.FacultyID,
                    teacher.Position
                    );
                return Ok(new { message = "Giáo viên mới đã được thêm thành công." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi thêm giáo viên.", details = ex.Message });

            }
        }
        // PUT: api/TeacherApi/5
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.TeacherID)
            {
                return BadRequest(new { message = "ID trong URL và ID trong body không khớp." });
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL UpdateTeacher({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                    teacher.TeacherID,
                    teacher.FirstName,
                    teacher.LastName,
                    teacher.Gender,
                    teacher.DateOfBirth,
                    teacher.Phone,
                    teacher.Email,
                    teacher.FacultyID,
                    teacher.Position
                    );
                return Ok(new { message = "Giáo viên đã được cập nhật thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật giáo viên.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    $"CALL DeleteTeacher({id})"
                    );
                return Ok(new { message = "Giáo viên đã được xóa thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa giáo viên.", details = ex.Message });
            }
        }
    }
}