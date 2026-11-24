using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AnCuManagement.DAL
{
    /// <summary>
    /// Lớp tiện ích quản lý kết nối và thực thi truy vấn Cơ sở dữ liệu qua ADO.NET
    /// Do Nguyễn Thành Nam (MSSV: 24810310230) phụ trách phát triển.
    /// </summary>
    public static class DbHelper
    {
        private static string _connectionString = @"Data Source=.\MSSQLSERVER01;Initial Catalog=QuanLyChungCuAnCu;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;";

        static DbHelper()
        {
            InitConnectionString();
        }

        private static void InitConnectionString()
        {
            try
            {
                string? configConn = ConfigurationManager.ConnectionStrings["QuanLyChungCuAnCu"]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(configConn))
                {
                    if (TestConnection(configConn))
                    {
                        _connectionString = configConn;
                        return;
                    }
                }
            }
            catch { }

            // Tự động dò các SQL Server Instance phổ biến trên máy tính
            string[] candidates = new[] {
                @".\MSSQLSERVER01",
                @"localhost",
                @".\SQLEXPRESS",
                @"(local)",
                @"."
            };

            foreach (var server in candidates)
            {
                // Thêm Connect Timeout=2 để dò tìm nhanh các instance SQL Server khả dụng
                string candidateConn = $@"Data Source={server};Initial Catalog=QuanLyChungCuAnCu;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;Connect Timeout=2;";
                if (TestConnection(candidateConn))
                {
                    _connectionString = candidateConn;
                    return;
                }
            }
        }

        private static bool TestConnection(string connStr)
        {
            try
            {
                string testConnStr = connStr;
                if (!testConnStr.Contains("Connect Timeout", StringComparison.OrdinalIgnoreCase) &&
                    !testConnStr.Contains("Connection Timeout", StringComparison.OrdinalIgnoreCase))
                {
                    testConnStr = testConnStr.TrimEnd(';') + ";Connect Timeout=2;";
                }

                using var conn = new SqlConnection(testConnStr);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Cập nhật chuỗi kết nối linh hoạt từ App.config hoặc cấu hình hệ thống
        /// </summary>
        public static void SetConnectionString(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                _connectionString = connectionString;
            }
        }

        /// <summary>
        /// Lấy chuỗi kết nối hiện tại
        /// </summary>
        public static string GetConnectionString() => _connectionString;

        /// <summary>
        /// Khởi tạo và trả về đối tượng SqlConnection
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Thực thi Stored Procedure trả về bảng dữ liệu DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string spName, SqlParameter[]? parameters = null)
        {
            var dataTable = new DataTable();
            using var conn = GetConnection();
            using var cmd = new SqlCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            CleanDataTable(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Thực thi Stored Procedure thay đổi dữ liệu (INSERT, UPDATE, DELETE)
        /// </summary>
        public static int ExecuteNonQuery(string spName, SqlParameter[]? parameters = null)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Thực thi Stored Procedure trả về giá trị đơn ô (Scalar)
        /// </summary>
        public static object? ExecuteScalar(string spName, SqlParameter[]? parameters = null)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Thực thi câu lệnh SQL trực tiếp (Text) trả về bảng dữ liệu DataTable
        /// </summary>
        public static DataTable ExecuteQueryText(string sql, SqlParameter[]? parameters = null)
        {
            var dataTable = new DataTable();
            using var conn = GetConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            CleanDataTable(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Thực thi câu lệnh SQL trực tiếp (INSERT, UPDATE, DELETE) không qua Stored Procedure
        /// </summary>
        public static int ExecuteNonQueryText(string sql, SqlParameter[]? parameters = null)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            if (parameters != null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters);
            }
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Tu dong kiem tra va khoi tao lai CSDL sach 100% neu phat hien CSDL cu bi loi font
        /// </summary>
        public static void ChuanHoaFontKhongDauNeuCan()
        {
            DbInitializer.KiemTraVaKhoiTaoTrietDe();
        }

        /// <summary>
        /// Khử sạch toàn bộ ký tự lỗi font mã hóa (mojibake) trên bất kỳ DataTable nào được trả về từ CSDL
        /// </summary>
        public static void CleanDataTable(DataTable? dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            for (int col = 0; col < dt.Columns.Count; col++)
            {
                if (dt.Columns[col].DataType == typeof(string))
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        object val = dt.Rows[r][col];
                        if (val != null && val != DBNull.Value)
                        {
                            string s = val.ToString()!;
                            string cleaned = SanitizeMojibake(s);
                            if (!string.Equals(s, cleaned, StringComparison.Ordinal))
                            {
                                dt.Rows[r][col] = cleaned;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Xu ly chuyen doi cac chuoi bi loi encoding CP1252/Latin1 sang Tieng Viet khong dau sach se
        /// </summary>
        public static string SanitizeMojibake(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            // Kiem tra nhanh xem chuoi co chua ky tu loi font khong
            if (!s.Contains('Ä') && !s.Contains('Ã') && !s.Contains('á') && !s.Contains('»') && 
                !s.Contains('º') && !s.Contains('²') && !s.Contains('¿') && !s.Contains('¡') && 
                !s.Contains('Â') && !s.Contains('Æ') && !s.Contains("fn"))
            {
                return s;
            }

            // 1. Cac cum tu trang thai va phong co ban
            if (s.Contains("ÄangThu") || s.Contains("ÄangThuÃª")) return "DangThue";
            if (s.Contains("Trá»‘ng") || s.Contains("Trá»")) return "Trong";
            if (s.Contains("Sá»­a") || s.Contains("Sua")) return "SuaChua";
            if (s.Contains("Äáº·t")) return "DatCoc";
            if (s.Contains("PhÃ²ng ÄÆ¡n") || s.Contains("PhÃ²ng Ä")) return "Phong Don";
            if (s.Contains("PhÃ²ng ÄÃ´i")) return "Phong Doi";

            // 2. Thay the cac cum tu thuong gap
            var replaces = new (string Key, string Val)[]
            {
                ("CÃfn", "Can"),
                ("CÄfn", "Can"),
                ("Äá»— Diá»‡u Linh", "Do Dieu Linh"),
                ("Äá»— Diá»‡u Li", "Do Dieu Linh"),
                ("Äáº·ng Tiáº¿n Ä", "Dang Tien Dat"),
                ("Äáº·ng Tiáº¿n", "Dang Tien Dat"),
                ("Äinh Mai Anh", "Dinh Mai Anh"),
                ("BÃ¹i Thá»¹ Trang", "Bui Thuy Trang"),
                ("BÃ¹i Thá»¹", "Bui Thuy Trang"),
                ("LÃ½ VÄfn Háº-u", "Ly Van Hau"),
                ("HoÃ ng Cáº©m", "Hoang Cam Tu"),
                ("Pháº¡m Quang", "Pham Quang Sang"),
                ("Pháº¡m Thu Dung", "Pham Thu Dung"),
                ("Pháº¡m VÄfn S", "Pham Van Son"),
                ("Nguyá»...n Äá»...", "Nguyen Duc Trong"),
                ("Nguyá»...n KhÃ¡...", "Nguyen Khanh Huyen"),
                ("Nguyá»...n", "Nguyen"),
                ("Nguyá»", "Nguyen"),
                ("HÃ  VÄfn Ã", "Ha Van Y"),
                ("TÃ¡Âºnh Xuân", "Ta Hong Xuan"),
                ("LÃ¢m Quá»‘c", "Lam Quoc Vinh"),
                ("LÃª TÃ¢n", "Le Tan Phat"),
                ("Cao VÄfn ThÃ¡", "Cao Van Thang"),
                ("Cao Thi Mo", "Cao Thi Mo"),
                ("Chu Thá»‹ Sen", "Chu Thi Sen"),
                ("Chu Thá»‹", "Chu Thi Sen"),
                ("Trinh Van Quan", "Trinh Van Quan"),
                ("Vo Gia Huy", "Vo Gia Huy"),
                ("Vuong Thi Uyen", "Vuong Thi Uyen"),
                ("Vo Kim Ngan", "Vo Kim Ngan"),
                ("Bui Minh Khang", "Bui Minh Khang"),
                ("Dinh Thi Lan", "Dinh Thi Lan"),
                ("Dinh Van Tien", "Dinh Van Tien"),
                ("Do Thi Giang", "Do Thi Giang"),
                ("Do Van Hung", "Do Van Hung"),
                ("Doan Thi Nga", "Doan Thi Nga"),
                ("Duong Hoai Phuc", "Duong Hoai Phuc"),
                ("HÃ  Ná»™i", "Ha Noi"),
                ("Thanh HÃ³a", "Thanh Hoa"),
                ("Ninh BÃ¬nh", "Ninh Binh"),
                ("Quáº£ng Ninh", "Quang Ninh"),
                ("Nghá»‡ An", "Nghe An"),
                ("Nam Äá»‹nh", "Nam Dinh"),
                ("ThÃ¡i NguyÃªn", "Thai Nguyen"),
                ("TÃ¢y Há»“", "Tay Ho"),
                ("BÃ¡c Ninh", "Bac Ninh"),
                ("Hai Duong", "Hai Duong"),
                ("Hung Yen", "Hung Yen"),
                ("Lang Son", "Lang Son"),
                ("Da Nang", "Da Nang"),
                ("Phu Tho", "Phu Tho"),
                ("Bac Giang", "Bac Giang"),
                ("Thai Binh", "Thai Binh"),
                ("Vinh Phuc", "Vinh Phuc"),
                ("HÃ¡ÂºÂ£i PhÃ²ng", "Hai Phong"),
                ("ÄÃ¨n chiáº¿u", "Den chieu sang"),
                ("BÃ³ng ÄÃ¨n", "Bong den LED"),
                ("ÄÃ£ náº¡p", "Da nap bo sung"),
                ("Ká»¹ thuáº-t", "Ky thuat vien"),
                ("Vá»›i", "Voi"),
                ("Äang", "Dang"),
                ("Ä", "D"),
                ("Ã", "a"),
                ("á", "a"),
                ("Â", "A"),
                ("Æ", ""),
                ("º", ""),
                ("»", ""),
                ("¿", ""),
                ("¡", ""),
                ("²", ""),
                ("´", ""),
                ("`", ""),
                ("ˆ", ""),
                ("~", "")
            };

            string res = s;
            foreach (var (k, v) in replaces)
            {
                if (res.Contains(k))
                {
                    res = res.Replace(k, v);
                }
            }
            return res.Trim();
        }

        /// <summary>
        /// Thực thi một khối lệnh nghiệp vụ trong một SqlTransaction an toàn, trả về kết quả
        /// </summary>
        public static T ExecuteTransaction<T>(Func<SqlConnection, SqlTransaction, T> work)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                T result = work(conn, transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Thực thi một khối lệnh nghiệp vụ trong một SqlTransaction an toàn (không trả về kết quả)
        /// </summary>
        public static void ExecuteTransaction(Action<SqlConnection, SqlTransaction> work)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                work(conn, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Thực thi danh sách câu lệnh Stored Procedure trong một SqlTransaction an toàn
        /// </summary>
        public static int ExecuteNonQueryWithTransaction(List<(string spName, SqlParameter[]? parameters)> commands)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                int affectedRows = 0;
                foreach (var (spName, parameters) in commands)
                {
                    using var cmd = new SqlCommand(spName, conn, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    affectedRows += cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return affectedRows;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
