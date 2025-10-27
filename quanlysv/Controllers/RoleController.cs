using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;

public class RoleController : Controller
{
    private readonly ApplicationDbContext _context;

    public RoleController(ApplicationDbContext context)
    {
        _context = context;
    }
    // danh sach vai trò
    public async Task<IActionResult> Index()
    {
        var roles = await _context.Roles
            .FromSqlRaw("CALL GetAllRoles()")
            .AsNoTracking()
            .ToListAsync();
        return View(roles);
    }
    // phuong thuc ho tro lay vai tro theo ID
    private Role GetRoleById(int id)
    {
        var roleModel = _context.Roles
           .FromSqlRaw($"CALL GetRoleById({id})")
           .AsNoTracking()
           .ToList() //  dùng ToList() hoặc ToListAsync() để nhận kết quả
           .FirstOrDefault(); // lấy phần tử đầu tiên
        return roleModel;
    }
    // Thêm vai trò (GET)
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    // Thêm vai trò (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Role role)
    {
        if (ModelState.IsValid)
        {
            _context.Database.ExecuteSqlRaw(
                "CALL AddRole({0}, {1})",
                role.RoleName,
                role.Description
            );
            return RedirectToAction("Index");
        }
        return View(role);
    }
    // Chỉnh sửa vai trò (GET)
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var roleModel = GetRoleById(id);
        if (roleModel == null) return NotFound();
        return View(roleModel);
    }
    // Chỉnh sửa vai trò (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Role role)
    {
        if (id != role.RoleID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                    "CALL UpdateRole({0}, {1}, {2})",
                    role.RoleID,
                    role.RoleName,
                    role.Description
                );
            }
            catch (DbUpdateException)
            {
                return View(role);
            }
            return RedirectToAction("Index");
        }
        return View(role);
    }
    // Xóa vai trò (GET)
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var roleModel = GetRoleById(id);
        if (roleModel == null) return NotFound();
        return View(roleModel);
    }
    // Xóa vai trò (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _context.Database.ExecuteSqlRaw("CALL DeleteRole({0})", id);
        return RedirectToAction("Index");
    }
}

