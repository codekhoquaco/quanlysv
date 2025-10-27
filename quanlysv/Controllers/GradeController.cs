using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using quanlysv;
using RestSharp;
using System.Text.Json;

public class GradeController : Controller
    {
    private readonly string apiBaseUrl = Config_Info.APIURL;
    private readonly ApplicationDbContext _context;
    public GradeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest("api/GradeApi/GetAllGrades", Method.Get);
        Console.WriteLine(client.BuildUri(request)); // debug URL
        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            ViewBag.Error = "Không thể tải danh sách điểm.";
            return View(new List<Grade>());
        }
        var grades = JsonSerializer.Deserialize<List<Grade>>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return View(grades);
    }
    private Grade GetGradeById(int id)
    {
        var gradeModel = _context.Grades
           .FromSqlRaw($"CALL GetGradeById({id})")
           .AsNoTracking()
           .ToList()
           .FirstOrDefault();
        return gradeModel;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Grade grade)
    {
        if (!ModelState.IsValid)
            return View(grade);

        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest("api/GradeApi/Add", Method.Post);
        request.AddJsonBody(grade);

        var response = client.Execute(request);

        if (response.IsSuccessful)
            return RedirectToAction(nameof(Index));

        ViewBag.Error = $"Không thể thêm môn học mới. Chi tiết: {response.Content ?? "Không có thông tin lỗi"}";
        return View(grade);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/GradeApi/{id}", Method.Get);
        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            return NotFound();
        }
        var grade = JsonSerializer.Deserialize<Grade>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return View(grade);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("GradeID,EnrollmentID,Score,GradeType,DateRecorded")] Grade grade)
    {
        if (id != grade.GradeID)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var client = new RestClient(apiBaseUrl);
            var request = new RestRequest($"api/GradeApi/Edit/{id}", Method.Put);
            request.AddJsonBody(grade);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Lỗi khi cập nhật điểm.");
            }
        }
        return View(grade);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/GradeApi/Delete/{id}", Method.Delete);
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.Error = "Lỗi khi xóa điểm.";
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/GradeApi/{id}", Method.Get);
        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            return NotFound();
        }
        var grade = JsonSerializer.Deserialize<Grade>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return View(grade);
    }
}

