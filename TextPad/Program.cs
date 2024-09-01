using System.Reflection;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Windows.Forms;

namespace TextPad
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}



//namespace TextPad
//{
//    internal static class Program
//    {
//        private static readonly string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();

//        [STAThread]
//        static void Main()
//        {
//            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
//            {
//                if (!mutex.WaitOne(0, false))
//                {
//                    // Bringe die bereits laufende Anwendung in den Vordergrund
//                    BringRunningAppToFront();
//                    return;
//                }

//                // To customize application configuration such as set high DPI settings or default font,
//                // see https://aka.ms/applicationconfiguration.
//                ApplicationConfiguration.Initialize();
//                Application.Run(new Form1());
//                //Application.EnableVisualStyles();
//                //Application.SetCompatibleTextRenderingDefault(false);
//                //Application.Run(new Form1());
//            }
//        }

//        private static void BringRunningAppToFront()
//        {
//            IntPtr hWnd = FindWindow(null, "Text Pad App");
//            if (hWnd != IntPtr.Zero)
//            {
//                SetForegroundWindow(hWnd);
//            }
//        }

//        [DllImport("user32.dll", CharSet = CharSet.Auto)]
//        private static extern IntPtr FindWindow(string strClassName, string strWindowName);

//        [DllImport("user32.dll")]
//        private static extern bool SetForegroundWindow(IntPtr hWnd);
//        //[LibraryImport("user32.dll")]
//        //private static extern IntPtr FindWindow(string strClassName, string strWindowName);

//        //[LibraryImport("user32.dll")]
//        //private static extern bool SetForegroundWindow(IntPtr hWnd);
//    }
//}