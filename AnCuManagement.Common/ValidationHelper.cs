using System.Text.RegularExpressions;

namespace AnCuManagement.Common
{
    /// <summary>
    /// Lớp tiện ích kiểm tra tính hợp lệ của dữ liệu nhập vào.
    /// Hỗ trợ kiểm tra CCCD, số điện thoại, email, và các giá trị số.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Kiểm tra số Căn cước công dân (CCCD) hợp lệ.
        /// CCCD hợp lệ phải có đúng 12 chữ số.
        /// </summary>
        /// <param name="cccd">Số CCCD cần kiểm tra</param>
        /// <returns>True nếu CCCD hợp lệ</returns>
        public static bool IsValidCCCD(string cccd)
        {
            if (string.IsNullOrWhiteSpace(cccd))
                return false;

            // CCCD phải có đúng 12 chữ số
            return Regex.IsMatch(cccd, @"^\d{12}$");
        }

        /// <summary>
        /// Kiểm tra số điện thoại Việt Nam hợp lệ.
        /// Số điện thoại hợp lệ phải có đúng 10 chữ số và bắt đầu bằng số 0.
        /// </summary>
        /// <param name="sdt">Số điện thoại cần kiểm tra</param>
        /// <returns>True nếu số điện thoại hợp lệ</returns>
        public static bool IsValidSoDienThoai(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
                return false;

            // Số điện thoại Việt Nam: 10 chữ số, bắt đầu bằng 0
            return Regex.IsMatch(sdt, @"^0\d{9}$");
        }

        /// <summary>
        /// Kiểm tra địa chỉ email hợp lệ (kiểm tra cơ bản).
        /// </summary>
        /// <param name="email">Địa chỉ email cần kiểm tra</param>
        /// <returns>True nếu email hợp lệ</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Biểu thức chính quy kiểm tra email cơ bản
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Kiểm tra giá trị số có phải là số dương hay không.
        /// </summary>
        /// <param name="value">Giá trị cần kiểm tra</param>
        /// <returns>True nếu giá trị lớn hơn 0</returns>
        public static bool IsPositiveNumber(decimal value)
        {
            return value > 0;
        }

        /// <summary>
        /// Kiểm tra chuỗi không null và không rỗng.
        /// </summary>
        /// <param name="value">Chuỗi cần kiểm tra</param>
        /// <returns>True nếu chuỗi có giá trị</returns>
        public static bool IsNotNullOrEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
