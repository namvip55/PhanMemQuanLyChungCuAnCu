using ClosedXML.Excel;
using System.Data;
using System.Reflection;

namespace AnCuManagement.Common
{
    /// <summary>
    /// Lớp tiện ích xuất dữ liệu ra file Excel (.xlsx) sử dụng thư viện ClosedXML.
    /// Hỗ trợ xuất từ DataTable hoặc danh sách đối tượng (List&lt;T&gt;).
    /// </summary>
    public static class ExcelExportHelper
    {
        /// <summary>
        /// Xuất dữ liệu từ DataTable ra file Excel.
        /// Dòng tiêu đề (header) được in đậm để dễ phân biệt.
        /// </summary>
        /// <param name="dt">DataTable chứa dữ liệu cần xuất</param>
        /// <param name="filePath">Đường dẫn file Excel đầu ra (.xlsx)</param>
        /// <param name="sheetName">Tên sheet trong file Excel (mặc định: "Sheet1")</param>
        public static void ExportDataTableToExcel(DataTable dt, string filePath, string sheetName = "Sheet1")
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                // Ghi dòng tiêu đề (header) từ tên các cột trong DataTable
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    var cell = worksheet.Cell(1, col + 1);
                    cell.Value = dt.Columns[col].ColumnName;
                    // Định dạng tiêu đề: in đậm
                    cell.Style.Font.Bold = true;
                }

                // Ghi dữ liệu từng dòng
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        var value = dt.Rows[row][col];
                        var cell = worksheet.Cell(row + 2, col + 1);

                        // Xử lý giá trị null
                        if (value == null || value == DBNull.Value)
                        {
                            cell.Value = string.Empty;
                        }
                        else if (value is decimal decVal)
                        {
                            cell.Value = decVal;
                        }
                        else if (value is double dblVal)
                        {
                            cell.Value = dblVal;
                        }
                        else if (value is int intVal)
                        {
                            cell.Value = intVal;
                        }
                        else if (value is long longVal)
                        {
                            cell.Value = longVal;
                        }
                        else if (value is DateTime dtVal)
                        {
                            cell.Value = dtVal;
                            cell.Style.DateFormat.Format = "dd/MM/yyyy";
                        }
                        else
                        {
                            cell.Value = value.ToString();
                        }
                    }
                }

                // Tự động điều chỉnh độ rộng cột theo nội dung
                worksheet.Columns().AdjustToContents();

                // Lưu file Excel
                workbook.SaveAs(filePath);
            }
        }

        /// <summary>
        /// Xuất danh sách đối tượng (List&lt;T&gt;) ra file Excel bằng cách sử dụng Reflection.
        /// Các thuộc tính public của đối tượng sẽ trở thành các cột trong Excel.
        /// Dòng tiêu đề (header) được in đậm.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của đối tượng trong danh sách</typeparam>
        /// <param name="list">Danh sách đối tượng cần xuất</param>
        /// <param name="filePath">Đường dẫn file Excel đầu ra (.xlsx)</param>
        /// <param name="sheetName">Tên sheet trong file Excel (mặc định: "Sheet1")</param>
        public static void ExportListToExcel<T>(List<T> list, string filePath, string sheetName = "Sheet1")
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                // Lấy danh sách các thuộc tính public của kiểu T
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Ghi dòng tiêu đề từ tên các thuộc tính
                for (int col = 0; col < properties.Length; col++)
                {
                    var cell = worksheet.Cell(1, col + 1);
                    cell.Value = properties[col].Name;
                    // Định dạng tiêu đề: in đậm
                    cell.Style.Font.Bold = true;
                }

                // Ghi dữ liệu từng đối tượng trong danh sách
                for (int row = 0; row < list.Count; row++)
                {
                    for (int col = 0; col < properties.Length; col++)
                    {
                        var value = properties[col].GetValue(list[row]);
                        var cell = worksheet.Cell(row + 2, col + 1);

                        // Xử lý giá trị null
                        if (value == null)
                        {
                            cell.Value = string.Empty;
                        }
                        else if (value is decimal decVal)
                        {
                            cell.Value = decVal;
                        }
                        else if (value is double dblVal)
                        {
                            cell.Value = dblVal;
                        }
                        else if (value is int intVal)
                        {
                            cell.Value = intVal;
                        }
                        else if (value is long longVal)
                        {
                            cell.Value = longVal;
                        }
                        else if (value is DateTime dtVal)
                        {
                            cell.Value = dtVal;
                            cell.Style.DateFormat.Format = "dd/MM/yyyy";
                        }
                        else
                        {
                            cell.Value = value.ToString();
                        }
                    }
                }

                // Tự động điều chỉnh độ rộng cột theo nội dung
                worksheet.Columns().AdjustToContents();

                // Lưu file Excel
                workbook.SaveAs(filePath);
            }
        }
    }
}
