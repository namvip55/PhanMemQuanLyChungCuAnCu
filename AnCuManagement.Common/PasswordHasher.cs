using System.Security.Cryptography;
using System.Text;

namespace AnCuManagement.Common
{
    /// <summary>
    /// Lớp tiện ích để mã hóa và xác thực mật khẩu người dùng.
    /// Sử dụng thuật toán SHA256 kết hợp với salt ngẫu nhiên.
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Tạo chuỗi salt ngẫu nhiên 16 byte, trả về dạng Base64.
        /// Salt được dùng để tăng cường bảo mật cho mật khẩu.
        /// </summary>
        /// <returns>Chuỗi salt dạng Base64</returns>
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Mã hóa mật khẩu bằng thuật toán SHA256 kết hợp với salt.
        /// Công thức: SHA256(salt + password) → Base64
        /// </summary>
        /// <param name="password">Mật khẩu gốc cần mã hóa</param>
        /// <param name="salt">Chuỗi salt dạng Base64</param>
        /// <returns>Chuỗi hash dạng Base64</returns>
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Ghép salt + password rồi mã hóa
                string combined = salt + password;
                byte[] combinedBytes = Encoding.UTF8.GetBytes(combined);
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Xác thực mật khẩu bằng cách so sánh hash của mật khẩu nhập vào với hash đã lưu.
        /// </summary>
        /// <param name="password">Mật khẩu người dùng nhập vào</param>
        /// <param name="salt">Chuỗi salt đã lưu trong cơ sở dữ liệu</param>
        /// <param name="hash">Chuỗi hash đã lưu trong cơ sở dữ liệu</param>
        /// <returns>True nếu mật khẩu đúng, False nếu sai</returns>
        public static bool VerifyPassword(string password, string salt, string hash)
        {
            // Mã hóa mật khẩu nhập vào với cùng salt
            string computedHash = HashPassword(password, salt);
            // So sánh với hash đã lưu
            return computedHash == hash;
        }
    }
}
