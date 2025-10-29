using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SubjectApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        // get api/subject lay danh sach mon hoc
        [HttpGet]
        [Route("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _context.Subjects
                .FromSqlRaw("CALL GetAllSubjects()")
                .ToListAsync();
            return Ok(subjects);
        }
        // get api/subject/5 lay mon hoc theo id
        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            var subject = _context.Subjects
                .FromSqlRaw($"CALL GetSubjectById({id})")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }
        
        // get api/subject/5 lay danh sach khoa cho dropdown
        [HttpGet("FacultyList")]
        public async Task<IActionResult> GetFacultyList()
        {
            // Trả về một object ẩn danh chỉ chứa ID và Tên để tối ưu hóa dữ liệu
            var faculties = await _context.Faculties
                .Select(f => new
                {
                    FacultyID = f.FacultyID,
                    FacultyName = f.FacultyName + " (" + f.FacultyID + ")"
                })
                .ToListAsync();

            // Trả về danh sách dưới dạng JSON
            return Ok(faculties);
        }
        // post api/subject them mon hoc moi
        [HttpPost("Create")]
        public async Task<IActionResult> AddSubject([FromBody] Subject model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddSubject({0}, {1}, {2})",
                    model.SubjectName,
                    model.Credits,
                    model.FacultyID
                );
                return Ok(new { message = " Thêm môn học thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi thêm môn học: " + ex.Message });
            }
        }
        //put api/subject sua mon hoc
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditSubject(int id, [FromBody] Subject model)
        {
            if (id != model.SubjectID)
                return BadRequest(new { message = "ID không khớp." });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var affectedRows = await _context.Database.ExecuteSqlRawAsync(
                    "CALL UpdateSubject({0}, {1}, {2}, {3})",
                    model.SubjectID,
                    model.SubjectName,
                    model.Credits,
                    model.FacultyID
                );
                if (affectedRows > 0)
                {
                    return Ok(new { message = "Cập nhật môn học thành công." });
                }
                else
                {
                    return StatusCode(500, new { message = "Cập nhật thất bại. Không có hàng nào bị ảnh hưởng." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi nhật môn học.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
        //delete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
           await _context.Database.ExecuteSqlRawAsync($"CALL DeleteSubject({id})");
            return Ok(new { message = "✅ Xóa môn học thành công" });
        }
    }
}
