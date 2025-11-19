using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

namespace quanlysv.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RoleApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get api/role lay danh sach vai tro
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _context.Roles
                .FromSqlRaw("CALL GetAllRoles()")
                .ToListAsync();
            return Ok(roles);
        }
        // get api/role/id/5 lay vai tro theo id
        [HttpGet("id/{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _context.Roles
                .FromSqlRaw($" CALL GetRoleById({id})")
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();
            if (role == null)
                return NotFound();
            return Ok(role);
        }

        // POST: /Role
        [HttpPost("Add")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddRole({0}, {1})",
                    role.RoleName,
                    role.Description
                );
                return Ok(new { message = "Vai trò mới đã được thêm thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Đã xảy ra lỗi khi thêm vai trò.",
                    details = ex.Message
                });
            }
        }
        // PUT: /Role/5
        [HttpPut]
        [Route("Edit/{id}")]

        public async Task<IActionResult> EditRole(int id, [FromBody] Role role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL UpdateRole({0}, {1}, {2})",
                    id,
                    role.RoleName,
                    role.Description
                );
                return Ok(new { message = "Cập nhật vai trò thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Đã xảy ra lỗi khi cập nhật vai trò.",
                    details = ex.Message
                });
            }
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL DeleteRole({0})",
                    id
                );
                return Ok(new { message = "Xóa vai trò thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Đã xảy ra lỗi khi xóa vai trò.",
                    details = ex.Message
                });
            }
        }
    }
}
