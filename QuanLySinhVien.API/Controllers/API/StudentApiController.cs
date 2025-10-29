using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

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

        // ✅ GET: api/StudentApi
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students
                .FromSqlRaw("CALL GetAllStudents()")
                .ToListAsync();
            return Ok(students);

        }

        //// ✅ GET: api/StudentApi/5
        //[HttpGet("{id}")]
        //public IActionResult GetStudentById(int id)
        //{
        //    var student = _context.Students
        //        .FromSqlRaw($"CALL GetStudentById({id})")
        //        .AsNoTracking()
        //        .AsEnumerable()
        //        .FirstOrDefault(); 

        //    if (student == null)
        //        return NotFound();

        //    return Ok(student);
        //}


        // ✅ POST: api/StudentApi
        [HttpPost("Create")]
        public async Task<IActionResult> AddStudent([FromBody] Student model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
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
                return Ok(new { message = " Thêm sinh viên thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi thêm khoa.", detail = ex.Message });
            }
        }

        // ✅ PUT: api/StudentApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student model)
        {
            if (id != model.StudentID)
                return BadRequest("Mã sinh viên không khớp");

            await _context.Database.ExecuteSqlRawAsync(
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
            return Ok(new { message = "✅ Cập nhật sinh viên thành công" });
        }

        // ✅ DELETE: api/StudentApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("CALL DeleteStudent({0})", id);
            return Ok(new { message = "🗑️ Xóa sinh viên thành công" });
        }
    }
}
 