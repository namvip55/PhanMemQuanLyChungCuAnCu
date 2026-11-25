using System.Globalization;

namespace AnCuManagement.Common
{
    /// <summary>
    /// Lớp tiện ích định dạng hiển thị dữ liệu theo chuẩn Việt Nam.
    /// Hỗ trợ định dạng tiền tệ VND, ngày tháng, và thời gian.
    /// </summary>
    public static class FormatHelper
    {
        // Thông tin văn hóa Việt Nam dùng cho định dạng số và ngày tháng
        private static readonly CultureInfo _vietnameseCulture = new CultureInfo("vi-VN");

        /// <summary>
        /// Định dạng số tiền theo chuẩn Việt Nam.
        /// Ví dụ: 1500000 → "1.500.000 đ"
        /// </summary>
        /// <param name="amount">Số tiền cần định dạng</param>
        /// <returns>Chuỗi tiền tệ có định dạng Việt Nam</returns>
        public static string FormatTienVND(decimal amount)
        {
            // Định dạng số với dấu chấm phân cách hàng nghìn, thêm ký hiệu "đ"
            return amount.ToString("#,0", _vietnameseCulture) + " đ";
        }

        /// <summary>
        /// Bí danh định dạng tiền tệ
        /// </summary>
        public static string FormatCurrency(decimal amount)
        {
            return FormatTienVND(amount);
        }

        /// <summary>
        /// Định dạng ngày theo kiểu Việt Nam: dd/MM/yyyy.
        /// Ví dụ: 2025-03-15 → "15/03/2025"
        /// </summary>
        /// <param name="date">Ngày cần định dạng</param>
        /// <returns>Chuỗi ngày dạng dd/MM/yyyy</returns>
        public static string FormatNgay(DateTime date)
        {
            return date.ToString("dd/MM/yyyy", _vietnameseCulture);
        }

        /// <summary>
        /// Định dạng ngày giờ theo kiểu Việt Nam: dd/MM/yyyy HH:mm.
        /// Ví dụ: 2025-03-15 14:30 → "15/03/2025 14:30"
        /// </summary>
        /// <param name="date">Ngày giờ cần định dạng</param>
        /// <returns>Chuỗi ngày giờ dạng dd/MM/yyyy HH:mm</returns>
        public static string FormatNgayGio(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm", _vietnameseCulture);
        }

        /// <summary>
        /// Định dạng tháng năm theo kiểu Việt Nam.
        /// Ví dụ: thang=1, nam=2025 → "Tháng 01/2025"
        /// </summary>
        /// <param name="thang">Tháng (1-12)</param>
        /// <param name="nam">Năm</param>
        /// <returns>Chuỗi tháng năm dạng "Tháng MM/yyyy"</returns>
        public static string FormatThangNam(int thang, int nam)
        {
            return $"Tháng {thang:D2}/{nam}";
        }
    }
}
