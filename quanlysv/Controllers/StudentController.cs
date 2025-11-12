using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using quanlysv;
using RestSharp;
using System.Text.Json;

namespace QuanLySinhVien.Controllers
{
    public class StudentController : Controller
    {
        private readonly string apiBaseUrl = Config_Info.APIURL;

        private ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Student
        public async Task<IActionResult> Index(string keyword, int? page)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/StudentApi/GetAllStudents", Method.Get);
            Console.WriteLine(client.BuildUri(request));

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Error = "Không thể tải danh sách sinh viên.";
                return View(new List<Student>());
            }

            var students = JsonSerializer.Deserialize<List<Student>>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            // Nếu có từ khóa thì lọc
            if (!string.IsNullOrEmpty(keyword))
            {
                students = students
                    .Where(s => (s.FirstName + " " + s.LastName)
                    .Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            // phan trang
            int pageSize = 5;
            int pageNumber = page ?? 1;
            
            var pagedData = students
                .OrderBy(s => s.StudentID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.Keyword = keyword;
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)students.Count / pageSize);

            return View(pagedData);
        }
        // GET: /Student/Create

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var classes = await _context.Classes
                .Select(c => new
                {
                    ClassID = c.ClassID,
                    ClassName = c.ClassName + " (" + c.ClassID + ")"
                })
                .ToListAsync();
            ViewBag.ClassList = new SelectList(classes, "ClassID", "ClassName");

            return View();
        }


        // POST: /Student/Create
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/StudentApi/Create", Method.Post);

            request.AddJsonBody(student);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Không thể thêm sinh viên mới.";
            return View(student);
        }


        // GET: /Student/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/StudentApi/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return NotFound();

            var student = JsonSerializer.Deserialize<Student>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Load danh sách lớp (giống Create)
            var classes = await _context.Classes
                .Select(c => new
                {
                    ClassID = c.ClassID,
                    ClassName = c.ClassName + " (" + c.ClassID + ")"
                })
                .ToListAsync();
            ViewBag.ClassList = new SelectList(classes, "ClassID", "ClassName");

            return View(student);
        }

        // POST: /Student/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentID)
                return NotFound();

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"{id}", Method.Put);
            request.AddJsonBody(student);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Không thể cập nhật sinh viên.";
            return View(student);
        }

        // GET: /Student/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/StudentApi/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return NotFound();

            var student = JsonSerializer.Deserialize<Student>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/StudentApi/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = "Không thể xóa sinh viên.";
            return RedirectToAction(nameof(Index));
        }
    }
}
