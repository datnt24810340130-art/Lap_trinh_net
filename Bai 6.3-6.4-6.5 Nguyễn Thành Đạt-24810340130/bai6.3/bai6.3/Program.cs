namespace bai6._3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["ProjectTaskDB"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối ProjectTaskDB trong App.config.",
                    "Thiếu cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dal = new DAL.ProjectTaskDal(connectionString);
            var bus = new BUS.ProjectTaskBus(dal);
            Application.Run(new Form1(bus));
        }
    }
}