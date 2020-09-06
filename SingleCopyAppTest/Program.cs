using ai.SingleCopyAppTest;
using System;
using System.Windows.Forms;

namespace SingleCopyAppTest
{
    static class Program
    {
        private static readonly SingleCopyApp singleCopyApp = new SingleCopyApp(new Guid("746213CF-343B-47C0-AD18-86C1951B4BFE"));

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (singleCopyApp.InitLock(Application.ProductName, true, true, false))
                return;

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
