using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using quanlysv;
using RestSharp;
using System.Text.Json;
public class ClassController : Controller
    {
        private readonly string apiBaseUrl = Config_Info.APIURL;

        public readonly ApplicationDbContext _context;
        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /Class
        public async Task<IActionResult> Index()
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/ClassApi/GetAllClasses", Method.Get);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(client.BuildUri(request)); // debug URL
            if (!response.IsSuccessful)
            {
                ViewBag.Error = "Không thể tải danh sách lớp.";
                return View(new List<Class>());
            }
            var classes = JsonSerializer.Deserialize<List<Class>>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(classes);
        }

        // GET: /Class/Create
        [HttpGet]
        public IActionResult AddClass()
        {
            // Lấy danh sách khoa (Giả định Faculty Model đã OK)
            var faculties = _context.Faculties
            .Select(f => new
            {
                FacultyID = f.FacultyID,
                FacultyName = f.FacultyName + " (" + f.FacultyID + ")"
            })
            .ToList();
            ViewData["FacultyList"] = new SelectList(faculties, "FacultyID", "FacultyName");

        // Lấy danh sách giáo viên (Giả định Teacher Model đã được sửa lỗi NotMapped)
        var teacher = _context.Teachers
        .Select(t => new
        {
            TeacherID = t.TeacherID,
            // Sử dụng tên thuộc tính mới để tránh xung đột (Ví dụ: FullName)
            FullName = t.FirstName + " " + t.LastName + " (" + t.TeacherID + ")"
        })
        .ToList();
        // Dùng FullName trong SelectList
        ViewData["TeacherList"] = new SelectList(teacher, "TeacherID", "FullName");

        return View();
        }
        // POST: /Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // SỬA: Chuyển sang Async Task<IActionResult>
        public async Task<IActionResult> AddClass(Class cls)
        {
            if (!ModelState.IsValid)
                return View(cls);

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/ClassApi/AddClass", Method.Post);

            request.AddJsonBody(cls);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));
            
            ViewBag.Error = $"Không thể thêm lớp mới. Chi tiết: {response.Content}";
            return View(cls);
        }
        // GET: /Class/Edit/5
        [HttpGet]
        public async Task<IActionResult> EditClass(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/ClassApi/EditClass", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return NotFound();

            var cls = JsonSerializer.Deserialize<Class>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(cls);
        }
        // POST: /Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClass(int id, Class model)
        {
            if (id != model.ClassID)
                return BadRequest();

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/ClassApi/EditClass", Method.Put);
            request.AddJsonBody(model);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Không thể cập nhật lớp.";
            return View(model);
        }
        // GET: /Class/Delete/5
        [HttpGet]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/ClassApi/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return NotFound();

            var cls = JsonSerializer.Deserialize<Class>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(cls);
        }

        // POST: /Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/ClassApi/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Không thể xóa lớp.";
            return RedirectToAction(nameof(Index));

        }
}

