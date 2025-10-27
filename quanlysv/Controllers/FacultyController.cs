using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLySinhVien.Models;
using quanlysv;
using RestSharp;
using System.Text.Json;

namespace QuanLySinhVien.Controllers
{
    public class FacultyController : Controller
    {
        private readonly string apiBaseUrl = Config_Info.APIURL;
        // GET: /Faculty

        public async Task<IActionResult> Index()
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/FacultyApi/GetAllFaculties", Method.Get);
            Console.WriteLine(client.BuildUri(request)); // debug URL

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Error = "Không thể tải danh sách khoa.";
                return View(new List<Faculty>());
            }
            var faculties = JsonSerializer.Deserialize<List<Faculty>>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(faculties);
        }

        // GET: /Faculty/Create
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        // POST: /Faculty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (!ModelState.IsValid)
                return View(faculty);

            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/FacultyApi/Add", Method.Post); // Sử dụng route "/Add"

            request.AddJsonBody(faculty);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));

            ViewBag.Error = $"Không thể thêm khoa mới. Chi tiết: {response.Content}"; // Hiển thị lỗi chi tiết
            return View(faculty);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/FacultyApi/id/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                return NotFound();
            }
            var faculty = JsonSerializer.Deserialize<Faculty>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (faculty == null)
                return NotFound();
            return View(faculty);
        }
        // POST: /Faculty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Faculty faculty)
        {
            if (id != faculty.FacultyID)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(faculty);
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest("api/FacultyApi/Edit", Method.Post);
            request.AddJsonBody(faculty);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return RedirectToAction(nameof(Index));
            ViewBag.Error = "Không thể cập nhật thông tin khoa.";
            return View(faculty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/FacultyApi/{id}", Method.Delete);
            Console.WriteLine(client.BuildUri(request)); // debug URL

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                TempData["Success"] = "Đã xóa khoa thành công.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = $"Không thể xóa khoa. Lỗi: {response.StatusCode} - {response.Content}";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Faculty/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/FacultyApi/id/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                return NotFound();
            }
            var faculty = JsonSerializer.Deserialize<Faculty>(response.Content!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (faculty == null)
                return NotFound();
            return View(faculty);
        }
    }
}
