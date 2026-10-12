namespace Bai4._7
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
         
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}