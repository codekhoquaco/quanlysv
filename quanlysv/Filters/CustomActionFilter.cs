using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuanLySinhVien.Models; 
using QuanLySinhVien.Helpers; 

namespace QuanLySinhVien.Filters
{
    // Lớp giả lập tham số hệ thống (ví dụ: bật/tắt xác thực 2 lớp)
    public static class SystemParam
    {
        public static bool GA_VERIFY = true;
    }

    /// Bộ lọc hành động tùy chỉnh dùng để kiểm tra:
    /// 1. Chống tấn công injection cơ bản.
    /// 2. Authentication (Người dùng đã đăng nhập chưa).
    /// 3. Authorization (Người dùng có quyền truy cập vào chức năng hay không).
    /// 4. Kiểm tra trạng thái tài khoản (đăng nhập trùng, 2FA).
    public class CustomActionFilter : ActionFilterAttribute
    {
        // Mã chức năng hiện tại (ví dụ: "STUDENT_VIEW", "SUBJECT_EDIT")
        public string FunctionCode { get; set; }

        // Có bật kiểm tra đăng nhập hay không
        public bool CheckAuthentication { get; set; } = true;

        // Có bật kiểm tra quyền hay không
        public bool CheckRight { get; set; } = true;

        // Có hiển thị breadcrumb điều hướng không
        public bool ShowNavigator { get; set; } = true;
        /// Phương thức được gọi TRƯỚC khi Action trong Controller thực thi.
        /// Sử dụng phiên bản đồng bộ để đảm bảo chuỗi thực thi không bị lỗi.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string currentUrl = context.HttpContext.Request.Path.Value;
            string method = context.HttpContext.Request.Method;

            //  1. Chống tấn công injection cơ bản (ví dụ: CSV Injection) 
            if (method == "POST")
            {
                foreach (var item in context.ActionArguments)
                {
                    // Kiểm tra nếu dữ liệu đầu vào là string và bắt đầu bằng ký tự nguy hiểm (=)
                    if (item.Value is string str && str.Trim().StartsWith("="))
                    {
                        context.Result = new JsonResult(new { error = "Dữ liệu đầu vào không hợp lệ hoặc chứa ký tự đặc biệt!" });
                        return;
                    }
                }
            }

            //  2. Kiểm tra Authentication (Người dùng đã đăng nhập chưa) 
            // Lấy UserInfo bằng cách gọi hàm async và ép đồng bộ (TẠM THỜI)
            // Cần OnActionExecutionAsync nếu muốn sử dụng async/await hoàn toàn
            var user = GetUserLoggedIn(context).GetAwaiter().GetResult();

            if (user == null && CheckAuthentication)
            {
                // Chưa đăng nhập, chuyển hướng đến trang Login
                context.Result = new RedirectResult("/Auth/Login");
                return;
            }

            //  3. Kiểm tra trạng thái tài khoản 
            if (user != null)
            {
                // Kiểm tra đăng nhập trùng (ví dụ: Token cũ bị vô hiệu)
                if (!AccountManager.IsLastestLoggedIn(user))
                {
                    context.Result = new RedirectResult("/Auth/LoginConflict");
                    return;
                }

                // Kiểm tra xác thực 2 lớp (2FA)
                if (SystemParam.GA_VERIFY && !user.GA_Confirmed)
                {
                    context.Result = new RedirectResult("/Auth/AccessDenied");
                    return;
                }
            }

            //  4. Xác định FunctionCode và Authorization 

            // Nếu FunctionCode chưa được gán thủ công trên Attribute, tự tìm theo URL
            if (string.IsNullOrEmpty(FunctionCode))
            {
                FunctionCode = GetFunctionByUrl(currentUrl);
            }

            if (user != null && CheckRight)
            {
                List<string> userRights = user.List_User_Functions ?? new List<string>();

                // Kiểm tra xem người dùng có FunctionCode này không
                if (!userRights.Contains(FunctionCode))
                {
                    // Không có quyền, chuyển hướng đến trang Access Denied
                    context.Result = new RedirectResult("/Auth/AccessDenied");
                    return;
                }
            }

            //  5. Hiển thị thanh điều hướng (breadcrumb) 
            if (ShowNavigator && !string.IsNullOrEmpty(FunctionCode) && context.Controller is Controller controller)
            {
                var navigator = BuildNavigator(FunctionCode);
                // Truyền dữ liệu Breadcrumb đến View thông qua ViewBag
                controller.ViewBag.Breadcrumb = navigator;
            }

            base.OnActionExecuting(context);
        }

        //  HÀM HỖ TRỢ 
        /// Giả lập hàm lấy thông tin người dùng đã đăng nhập từ Cookie/Session
        private async Task<UserInfo> GetUserLoggedIn(ActionExecutingContext context)
        {
            // Lấy username từ Session (Giả lập việc đã đăng nhập)
            var username = context.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
                return null;

            // Mô phỏng việc truy vấn dữ liệu từ DB/Cache
            await Task.Delay(10);
            // Gọi hàm từ AccountManager để lấy UserInfo và danh sách quyền
            return AccountManager.GetUserInfo(username);
        }
        /// Tự động tìm mã FunctionCode dựa trên URL.
        /// Ví dụ: "/Student/Edit" -> "STUDENT_EDIT"
        private string GetFunctionByUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            var parts = url.Trim('/').Split('/');

            // Đảm bảo có ít nhất Controller và Action
            if (parts.Length < 2) return "";

            // Loại bỏ các ID ở cuối URL (ví dụ: /Student/Edit/5)
            if (parts.Length > 2)
            {
                parts = parts.Take(2).ToArray();
            }

            return string.Join("_", parts).ToUpper();
        }
        /// Tạo chuỗi breadcrumb dựa trên FunctionCode.
        /// Ví dụ: "STUDENT_EDIT" -> "STUDENT / EDIT"
        private string BuildNavigator(string functionCode)
        {
            return functionCode.Replace("_", " / ");
        }
    }
}