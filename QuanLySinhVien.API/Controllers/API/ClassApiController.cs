using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllClasses")]
        public async Task<IActionResult> GetAllClasses()
        {
            var data = await _context.Classes
                .FromSqlRaw("CALL GetAllClasses()")
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cls = _context.Classes
                .FromSqlRaw($"CALL GetClassById({id})")
                .AsEnumerable()
                .FirstOrDefault();

            if (cls == null)
                return NotFound();

            return Ok(cls);
        }

        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass([FromBody] Class model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Database.ExecuteSqlRawAsync(
                "CALL AddClass({0}, {1}, {2}, {3})",
                model.ClassName,
                model.FacultyID,
                model.AcademicYear,
                model.HomeroomTeacher
            );

            return Ok(new { message = "Thêm lớp thành công" });
        }

        [HttpPut("EditClass/{id}")]
        public async Task<IActionResult> EditClass(int id, [FromBody] Class model)
        {
            if (id != model.ClassID)
                return BadRequest();

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

        [HttpDelete("DeleteClass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("CALL DeleteClass({0})", id);
                return Ok(new { message = "Xóa lớp thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Không thể xóa lớp. Có thể lớp đang được sử dụng bởi sinh viên.",
                    details = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

    }
}
