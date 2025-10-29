using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using quanlysv;
using RestSharp;
using System.Text.Json;


public class SubjectController : Controller
{
    private readonly string apiBaseUrl = Config_Info.APIURL;

    private readonly ApplicationDbContext _context;

    public SubjectController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult>Index()
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest("api/SubjectApi/GetAllSubjects", Method.Get);
        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            ViewBag.Error = "Không thể tải danh sách môn học.";
            return View(new List<Subject>());
        }
        var subjects = JsonSerializer.Deserialize<List<Subject>>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return View(subjects);
    }

    // --- Phương thức hỗ trợ: Lấy Môn học theo ID ---

    // --- 2. CREATE (GET) ---
    [HttpGet]
    public IActionResult Create()
    {
        var faculties = _context.Faculties
            .Select(f => new {
                FacultyID = f.FacultyID,
                FacultyName = f.FacultyName + " (" + f.FacultyID + ")"
                })
            .ToList();
        
        // Thường cần load danh sách Faculty cho DropdownList
        ViewData["FacultyList"] = new SelectList(faculties, "FacultyID", "FacultyName");
        return View();
    }
    //post 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Subject subject)
    {
        if (!ModelState.IsValid)
            return View(subject);

        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest("api/SubjectApi/Create", Method.Post);

        request.AddJsonBody(subject);
        var response = client.Execute(request);

        if (response.IsSuccessful)
            return RedirectToAction(nameof(Index));
        ViewBag.Error = $"Không thể thêm môn học mới. Chi tiết: {response.Content ?? "Không có thông tin lỗi"}";
        return View(subject);
    }
    //// --- 3. DETAILS ---
    //public IActionResult Details(int id)
    //{
    //    var subjectModel = GetSubjectById(id);
    //    if (subjectModel == null) return NotFound();
    //    return View(subjectModel);
    //}

    // --- 5. EDIT (GET) ---
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/SubjectApi/{id}", Method.Get);
        var response = client.Execute(request);
        if (!response.IsSuccessful)
            return NotFound();

        var subjectModel = JsonSerializer.Deserialize<Subject>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var faculties = _context.Faculties
            .Select(f => new {
                FacultyID = f.FacultyID,
                FacultyName = f.FacultyName + " (" + f.FacultyID + ")"
            })
            .ToList();
        // Thường cần load danh sách Faculty cho DropdownList
        ViewData["FacultyList"] = new SelectList(faculties, "FacultyID", "FacultyName");
        return View(subjectModel);
    }

    // --- 6. EDIT (POST) ---
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Subject subject)
    {
        if (id != subject.SubjectID)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(subject);
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/SubjectApi/Edit/{id}", Method.Put);
        request.AddJsonBody(subject);
        var response = client.Execute(request);
        if (response.IsSuccessful)
            return RedirectToAction(nameof(Index));
        ViewBag.Error = $"Không thể cập nhật môn học. Chi tiết: {response.Content ?? "Không có thông tin lỗi"}";
        return View(subject);
    }

    // --- 7. DELETE (GET) ---
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/SubjectApi/{id}", Method.Get);
        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            return NotFound();
        var subject = JsonSerializer.Deserialize<Subject>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(subject);
    }

    // --- 8. DELETE (POST) ---
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = new RestClient(apiBaseUrl);
        var request = new RestRequest($"api/SubjectApi/{id}", Method.Delete);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
            return RedirectToAction(nameof(Index));
             
        ViewBag.Error = $"Không thể xóa môn học.";
        return RedirectToAction(nameof(Index));
    }

}