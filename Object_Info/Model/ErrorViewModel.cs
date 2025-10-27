using System;

namespace quanlysv.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = string.Empty; // Khởi tạo mặc định để tránh CS8618

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorMessage { get; set; } = string.Empty; // Khởi tạo mặc định

        public int? ErrorCode { get; set; } // Sử dụng nullable int
    }
}