using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

// ✅ Đăng ký controller API
builder.Services.AddControllers();

// ✅ Cho phép CORS nếu cần (để web client gọi API này)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// ===============================
// 🔍 TEST KẾT NỐI DATABASE
// ===============================
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (db.Database.CanConnect())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Kết nối CSDL API thành công!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Không thể kết nối tới CSDL!");
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("⚠️ Lỗi kết nối DB: " + ex.Message);
    }
    finally
    {
        Console.ResetColor();
    }
}

// ===============================
// 🔧 PIPELINE
// ===============================
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
