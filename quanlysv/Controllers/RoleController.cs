using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLySinhVien.Models;
using QuanLySinhVien.Data;
using QuanLySinhVien.Filters;
using quanlysv;
using RestSharp;
using System.Text.Json;
using PagedList;
using Microsoft.AspNetCore.Authorization;


[CustomActionFilter(FunctionCode = "ROLE_VIEW", CheckAuthentication = true)]

public class RoleController : Controller
{
    private readonly string apiBaseUrl = Config_Info.APIURL;
    
    private readonly ApplicationDbContext _context;
    public RoleController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult>Index(string keyword, int? page)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest("api/RoleApi/GetAllRoles", Method.Get);
        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            ViewBag.Error = "Không thể tải danh sách vai trò.";
            return View(new List<Role>());
        }
        var roles = JsonSerializer.Deserialize<List<Role>>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        // Nếu có từ khóa thì lọc
        if (!string.IsNullOrEmpty(keyword))
        {
            roles = roles
                .Where(r => r.RoleName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        //phan trang 
        int pageSize = 5;
        int paageNumber = page ?? 1;
        var PageData = roles
            .OrderBy(r => r.RoleID)
            .Skip((paageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        ViewBag.Keyword = keyword;
        ViewBag.PageNumber = paageNumber;
        ViewBag.TotalPages = (int)Math.Ceiling((double)roles.Count / pageSize);
        return View(PageData);
    }
    [CustomActionFilter(FunctionCode = "ROLE_CREATE")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Role role)
    {
        if (!ModelState.IsValid)
            return View(role);
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest("api/RoleApi/Add", Method.Post);
        request.AddJsonBody(role);
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi thêm vai trò.");
            return View(role);
        }
    }

    [CustomActionFilter(FunctionCode = "ROLE_EDIT")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/RoleApi/id/{id}", Method.Get);
        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            return NotFound();

        var role = JsonSerializer.Deserialize<Role>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (role == null)
            return NotFound();
        return View(role);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Role role)
    {
        if (id != role.RoleID)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(role);
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/RoleApi/Edit/{id}", Method.Put);
        request.AddJsonBody(role);
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi cập nhật vai trò.");
            return View(role);
        }
    }
    [CustomActionFilter(FunctionCode = "ROLE_DELETE")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/RoleApi/id/{id}", Method.Get);
        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
            return NotFound();
        var role = JsonSerializer.Deserialize<Role>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (role == null)
            return NotFound();
        return View(role);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/RoleApi/Delete/{id}", Method.Delete);
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi xóa vai trò.");
            var getRequest = new RestRequest($"api/RoleApi/id/{id}", Method.Get);
            var getResponse = await client.ExecuteAsync(getRequest);
            var role = JsonSerializer.Deserialize<Role>(getResponse.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(role);
        }
    }
}

