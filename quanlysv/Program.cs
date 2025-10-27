using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ==========================
        // 1️⃣ CẤU HÌNH CHUỖI KẾT NỐI
        // ==========================
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Chuỗi kết nối 'DefaultConnection' không tìm thấy trong appsettings.json.");
        }

        // ==========================
        // 2️⃣ ĐĂNG KÝ DỊCH VỤ
        // ==========================
        builder.Services.AddHttpClient(); // Cho phép gọi API từ client (RestSharp, HttpClient)

        // Đăng ký DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );

        // Đăng ký Authentication (xác thực cookie)
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

        // Đăng ký Authorization (phân quyền)
        builder.Services.AddAuthorization();

        // Đăng ký Controller (cho cả MVC + API)
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers(); // Cho phép controller kiểu API

        // Đăng ký Session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddHttpContextAccessor();

        // ==========================
        // 3️⃣ XÂY DỰNG ỨNG DỤNG
        // ==========================
        var app = builder.Build();

        // Kiểm tra kết nối DB
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (!db.Database.CanConnect())
            {
                throw new InvalidOperationException("Không thể kết nối tới cơ sở dữ liệu.");
            }
            Console.WriteLine("✅ Kết nối CSDL thành công!");
        }

        // ==========================
        // 4️⃣ CẤU HÌNH PIPELINE
        // ==========================
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Thứ tự rất quan trọng 👇
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        // ==========================
        // 5️⃣ MAP ROUTE MVC + API
        // ==========================
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Cho phép API hoạt động
        app.MapControllers();

        // ==========================
        // 6️⃣ CHẠY ỨNG DỤNG
        // ==========================
        app.Run();
    }
}
