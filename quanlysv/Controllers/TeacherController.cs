using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using quanlysv;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;


namespace QuanLySinhVien.Controllers
{
    public class TeacherController : Controller
    {
        private readonly string apiBaseUrl = Config_Info.APIURL;
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. INDEX (Danh sách) ---
        public async Task<IActionResult>Index(string keyword,int? page)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/TeacherApi/GetAllTeachers", Method.Get);

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Error = "Không thể tải danh sách giáo viên.";
                return View(new List<Teacher>());
            }
            var teachers = JsonSerializer.Deserialize<List<Teacher>>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            // Nếu có từ khóa thì lọc
            if (!string.IsNullOrEmpty(keyword))
            {
                teachers = teachers
                    .Where(t => (t.FirstName + " " + t.LastName)
                    .Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            //phan trang
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var pagedData = teachers
                .OrderBy(t => t.TeacherID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            ViewBag.Keyword = keyword;
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)teachers.Count / pageSize);

            return View(pagedData);
        }
        // 2. CREATE (GET) 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var faculties = _context.Faculties
                .Select(f => new
                {
                    FacultyID = f.FacultyID,
                    FacultyName = f.FacultyName + " (" + f.FacultyID + ")"
                })
                .ToList();
            ViewData["FacultyList"] = new SelectList(faculties, "FacultyID", "FacultyName");
            return View();
        }

        //3. CREATE (POST) 
        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                // Tải lại SelectList nếu lỗi
                ViewData["FacultyList"] = new SelectList(_context.Faculties.ToList(), "FacultyID", "FacultyName", teacher.FacultyID);
                return View(teacher);
            }

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/TeacherApi/Add", Method.Post);

            request.AddJsonBody(teacher);
            var respone = await client.ExecuteAsync(request);

            if (respone.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = $"Không thể thêm giáo viên. Chi tiết: {respone.Content}";

            return View(teacher);
        }

        //5. EDIT (GET) 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/TeacherApi/{id}", Method.Get); // SỬA: Lấy từ API
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return NotFound();

            var teacher = JsonSerializer.Deserialize<Teacher>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Lấy SelectList từ DB cho form Edit
            var faculties = _context.Faculties
                .Select(f => new
                {
                    FacultyID = f.FacultyID,
                    FacultyName = f.FacultyName + " (" + f.FacultyID + ")"
                })
                .ToList();
            ViewData["FacultyList"] = new SelectList(_context.Faculties.ToList(),"FacultyID","FacultyName",teacher.FacultyID);
            return View(teacher);
        }

        //6. EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Teacher teacher)
        {
            if (id != teacher.TeacherID)
                return NotFound();

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/TeacherApi/{id}", Method.Put);
            request.AddJsonBody(teacher);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = $"Không thể cập nhật giáo viên. Chi tiết: {response.Content}";
            // Tải lại SelectList nếu lỗi
            ViewData["FacultyList"] = new SelectList(_context.Faculties.ToList(), "FacultyID", "FacultyName", teacher.FacultyID);
            return View(teacher);
        }

        //7. DELETE (GET) 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var requet = new RestRequest($"api/TeacherApi/{id}", Method.Get);
            var response = await client.ExecuteAsync(requet);

            if (!response.IsSuccessful)
                return NotFound();

            var teacher = JsonSerializer.Deserialize<Teacher>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(teacher);
        }

        // --- 8. DELETE (POST) ---
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeteleComfirmed(int id)
        {
            var client = new RestClient(apiBaseUrl);
            // SỬA ROUTING và METHOD: Gửi tới api/TeacherApi/5 bằng DELETE
            var request = new RestRequest($"api/TeacherApi/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = $"Không thể xóa giáo viên. Chi tiết: {response.Content}";
            return RedirectToAction(nameof(Index));
        }
    }
}