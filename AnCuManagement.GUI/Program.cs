using AnCuManagement.GUI.Forms;
using AnCuManagement.DAL;

namespace AnCuManagement.GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            ApplicationConfiguration.Initialize();

            // Tự động kiểm tra và chuẩn hóa CSDL sạch 100% không lỗi font nếu phát hiện dữ liệu cũ
            DbInitializer.KiemTraVaKhoiTaoTrietDe();

            Application.Run(new FormLogin());
        }
    }
}